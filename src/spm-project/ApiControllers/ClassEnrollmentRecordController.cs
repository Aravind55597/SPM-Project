using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPM_Project.CustomExceptions;
using SPM_Project.DataTableModels;
using SPM_Project.DTOs;
using SPM_Project.EntityModels;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassEnrollmentRecordController : ControllerBase
    {

        public IUnitOfWork _unitOfWork;

        public ClassEnrollmentRecordController (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpPost, Route("ApproveEnrollment", Name = "ApproveEnrollment")]

        public async Task<IActionResult> ApproveLearnerEnrollment([FromQuery] int learnerId, [FromQuery] int classId)
        {


            var response = await ApproveEnrollment(learnerId, classId);


            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            return Ok(responseJson);

        }


        [HttpPost, Route("DeclineEnrollment", Name = "DeclineEnrollment")]

        public async Task<IActionResult> DeclineLearnerEnrollment([FromQuery] int learnerId, [FromQuery] int classId)
        {


            var response = await DeclineEnrollment(learnerId, classId);


            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            return Ok(responseJson);

        }
        //DATATABLE-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost, Route("ClassEnrollmentRecordsDataTable", Name = "GetClassEnrollmentRecordsDataTable")]
        public async Task<IActionResult> GetClassEnrollmentRecordsDataTable([FromBody] DTParameterModel dTParameterModel)
        {

            //return the data 
            var response = await _unitOfWork.ClassEnrollmentRecordRepository.GetClassEnrollmentRecordsDataTable(dTParameterModel);

            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            return Ok(responseJson);
        }


        //Non Action Methods

        [NonAction]
        public async Task<CourseClassesDTO> ApproveEnrollment(int learnerId, int courseClassId)
        {

            //check if class exists ; otherwise return not found
            //return courseclass
            var courseClass = await _unitOfWork.CourseClassRepository.GetByIdAsync(courseClassId, "Course");
            var learner = await _unitOfWork.LMSUserRepository.GetByIdAsync(learnerId, "ClassEnrollmentRecord");
            if (courseClass == null)
            {
                throw new NotFoundException($"Course class of id {courseClassId} does not exist");
            }
            if (learner == null)
            {
                throw new NotFoundException($"learner not exist");
            }

            //check if class slots not full else reject
            if (await new CourseClassesController(_unitOfWork).CheckIfClassFull(courseClassId)) {
                throw new NotFoundException($"Class is full ");
            }
            //if slots not full, approve learner for slots 
            if (learner.Enrollments != null)
            {
                var currentenrollment = learner.Enrollments.Find(x => x.CourseClass.Id == courseClass.Id);
                currentenrollment.IsEnrollled = true;
            }
            else {
                learner.Enrollments = new List<ClassEnrollmentRecord>();
                learner.Enrollments.Add(new ClassEnrollmentRecord { 
                    LMSUser = learner,
                    CourseClass = courseClass,
                    Course = courseClass.Course,
                    IsEnrollled = true
                });
            }

          
            await _unitOfWork.CompleteAsync();
            return new CourseClassesDTO(courseClass);
        }

        [NonAction]
        public async Task<CourseClassesDTO> DeclineEnrollment(int learnerId, int courseClassId)
        {

            //check if class exists ; otherwise return not found
            //return courseclass
            var courseClass = await _unitOfWork.CourseClassRepository.GetByIdAsync(courseClassId, "Course");
            var learner = await _unitOfWork.LMSUserRepository.GetByIdAsync(learnerId,"ClassEnrollmentRecord");
            if (courseClass == null)
            {
                throw new NotFoundException($"Course class of id {courseClassId} does not exist");
            }
            if (learner == null)
            {
                throw new NotFoundException($"learner not exist");
            }

            var currentenrollment = await _unitOfWork.ClassEnrollmentRecordRepository.GetAllAsync(filter: f => f.CourseClass.Id == courseClassId && f.LMSUser.Id == learner.Id);


            if (currentenrollment.Count > 0)
            {
                await _unitOfWork.ClassEnrollmentRecordRepository.RemoveByIdAsync(currentenrollment[0].Id);
                //currentenrollment[0].IsEnrollled = false;
            }
            else
            {
                throw new NotFoundException($"enrollment record does not exist");
            }

            await _unitOfWork.CompleteAsync();
            return new CourseClassesDTO(courseClass);
        }
       

    }
}
