using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPM_Project.CustomExceptions;
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

<<<<<<< Updated upstream
                var notFoundExp = new NotFoundException("Class does not exist", errorDict);
=======
<<<<<<< Updated upstream
>>>>>>> Stashed changes

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

            var record = new ClassEnrollmentRecord
            {
                CourseClass = courseclass
                
            };

            user.Enrollments.Add(record);
            await _unitOfWork.CompleteAsync();

        }

=======
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
               
                currentenrollment[0].IsEnrollled = false;
            }
            else
            {
                throw new NotFoundException($"enrollment record does not exist");
            }

            await _unitOfWork.CompleteAsync();
            return new CourseClassesDTO(courseClass);
        }
       
>>>>>>> Stashed changes

    }
}
