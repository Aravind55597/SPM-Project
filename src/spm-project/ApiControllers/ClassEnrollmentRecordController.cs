using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPM_Project.CustomExceptions;
<<<<<<< Updated upstream
=======
using SPM_Project.DataTableModels;
using SPM_Project.DTOs;
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
        [HttpPost, Route("Add",Name = "AddClassEnrollmentRecord")]
        public async  Task<IActionResult> AddEnrollmentRecord([FromQuery] int classId)
        {

            var userId =await  _unitOfWork.LMSUserRepository.RetrieveCurrentUserIdAsync();

            var user =await  _unitOfWork.LMSUserRepository.GetByIdAsync(userId);

            //var courseClass = await _unitOfWork.CourseClassRepository.GetByIdAsync(classId); 

            await AddEnrollmentRecord(user,classId); 

            return Ok();
        }

        public async Task AddEnrollmentRecord(LMSUser user, int classId)
        {

            //firstly retrieve class from classservice (check if class exists)
           var courseclass =  await _unitOfWork.CourseClassRepository.GetByIdAsync(classId);
            if (courseclass != null)
            {
                if (courseclass.EndRegistration < DateTime.Today || courseclass.StartRegistration > DateTime.Today)
                {
                    var errorDict = new Dictionary<string, string>()
                    {
                        {"Class", $"Class of  {courseclass.Id} registration is over" }
                    };

                    var notFoundExp = new NotFoundException("Class registration period is over", errorDict);

                    throw notFoundExp;
                }
            }
            else {
                var errorDict = new Dictionary<string, string>()
                    {
                        {"Class", $"Class of  Id {courseclass.Id} does not exist" }
                    };

                var notFoundExp = new NotFoundException("Class does not exist", errorDict);

                throw notFoundExp;
            }
            //Secondly use classenrollmentrecordservice to check eligibility 
            if (!await new CoursesController(_unitOfWork).GetCourseEligiblity(user, courseclass.Course)) {
                var errorDict = new Dictionary<string, string>()
                    {
                        {"Class", $"Class of  Id {courseclass.Id} does not exist" }
                    };

                var notFoundExp = new NotFoundException("Class does not exist", errorDict);

                throw notFoundExp;
            }


            //check if enrolled 

            if (await _unitOfWork.ClassEnrollmentRecordRepository.hasEnrollmentRecord(user, courseclass))
            {
                var errorDict = new Dictionary<string, string>()
                    {
                        {"Class", $"User has class of Id {courseclass.Id}  exist" }
                    };

                var notFoundExp = new NotFoundException("User has existing enrollment record with this class", errorDict);

                throw notFoundExp;
            }
            //Create classenrollment record for the user
=======
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
>>>>>>> Stashed changes

            var record = new ClassEnrollmentRecord
            {
                CourseClass = courseclass
                
            };

            user.Enrollments.Add(record);
            await _unitOfWork.CompleteAsync();

        }

        //approve enrollment request 

        public async Task<IActionResult> ApproveEnrollmentRequest(LMSUser user, CourseClass courseclass)
        {

       
            //LMSUser learner, int classId
            //assign user to class
            await new UsersController(_unitOfWork).AssignLearnertoClass(user, courseclass.Id);

            //TODO: trigger notification for approval

            


            return Ok();

        }

        //decline enrollment request 

        public async Task<IActionResult> DeclineEnrollmentRequest(ClassEnrollmentRecord record)
        {


            record.Approved = false ;


            //TODO: trigger notification for decline

            //save changes
            await _unitOfWork.CompleteAsync();


            return Ok();

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
