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
            var learner = await _unitOfWork.LMSUserRepository.GetByIdAsync(learnerId);
            if (courseClass == null)
            {
                throw new NotFoundException($"Course class of id {courseClassId} does not exist");
            }
            if (learner == null)
            {
                throw new NotFoundException($"learner not exist");
            }
            var currentenrollment = learner.Enrollments.Find(x => x.CourseClass.Id == courseClass.Id);

            if (currentenrollment == null)
            {
                throw new NotFoundException($"Enrollment not exist");
            }
            if (currentenrollment!=null) {
                currentenrollment.IsEnrollled = true;
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
            var learner = await _unitOfWork.LMSUserRepository.GetByIdAsync(learnerId);
            if (courseClass == null)
            {
                throw new NotFoundException($"Course class of id {courseClassId} does not exist");
            }
            if (learner == null)
            {
                throw new NotFoundException($"learner not exist");
            }
            var currentenrollment = learner.Enrollments.Find(x => x.CourseClass.Id == courseClass.Id);

            if (currentenrollment == null)
            {
                throw new NotFoundException($"Enrollment not exist");
            }
            if (currentenrollment != null)
            {
                currentenrollment.IsEnrollled = false;
            }
            await _unitOfWork.CompleteAsync();
            return new CourseClassesDTO(courseClass);
        }





    }
}
