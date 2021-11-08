using Microsoft.AspNetCore.Mvc;
using SPM_Project.CustomExceptions;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.DTOs;
using SPM_Project.DTOs.RRModels;
using SPM_Project.EntityModels;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPM_Project.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseClassesController : ControllerBase
    {
        public IUnitOfWork _unitOfWork;

        public CourseClassesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }




        //get the course Id
        //check if course exists
        //if it does not exists -> return 404 NOT FOUND
        //iF  it exists ,return classes

        //GET COURSE CLASSES-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet, Route("{id:int?}", Name = "GetCourseClasses")]
        public async Task<IActionResult> GetCourseClassesDTOAPIAsync(int? id, [FromQuery] int? courseId)
        {




            if (id != null && courseId != null)
            {
                throw new BadRequestException("You can either query a Class of a praticular Id OR all classes OR retreive classes for a course");
            }

            if (id != null)
            {
                return Ok(new Response<CourseClassesDTO>(await GetCourseClassDTOAsync(id.GetValueOrDefault())));
            }

            return Ok(new Response<List<CourseClassesDTO>>(await GetCourseClassesDTOAsync(courseId)));


        }

        [HttpPost, Route("AssignTrainerToClass", Name = "AssignTrainerToClass")]

        public async Task<IActionResult> AssignTrainerToClass([FromQuery] int trainerId, [FromQuery] int classId)
        {


            var response = await AssignTrainer(trainerId, classId);


            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            return Ok(responseJson);

        }

        [HttpPost, Route("WithdrawLearner", Name = "WithdrawLearner")]

        public async Task<IActionResult> WithdrawLearnerFromClass([FromQuery] int learnerId, [FromQuery] int classId)
        {


            var response = await WithdrawLearner(learnerId, classId);


            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            return Ok(responseJson);

        }


            [HttpPost, Route("AssignLearner", Name = "AssignLearner")]

            public async Task<IActionResult> AssignLearnerToClass ([FromQuery] int learnerId, [FromQuery] int classId)
            {


                var response = await AssignLearner(learnerId, classId);


                var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(response);
                return Ok(responseJson);

            }



            [HttpPost, Route("SubmitEnrollmentRequest", Name = "SubmitEnrollmentRequest")]
        public async Task<IActionResult> SubmitEnrollmentRequest([FromQuery] int classId, [FromQuery] int userid)
        {

            var userId = 0;
            if (userid == 0)
            {
                userId = await _unitOfWork.LMSUserRepository.RetrieveCurrentUserIdAsync();
            }
            else
            {

                userId = userid;
            }


            var user = await _unitOfWork.LMSUserRepository.GetByIdAsync(userId);

            //var courseClass = await _unitOfWork.CourseClassRepository.GetByIdAsync(classId); 

            await SubmitEnrollmentRequest(user, classId);

            return Ok();
        }

        [HttpPost, Route("GetApprovalStatus", Name = "GetApprovalStatus")]
        public async Task<IActionResult> GetApprovalStatus([FromQuery] int classId, [FromQuery] int userid)
        {

            var userId = 0;
            if (userid == 0)
            {
                userId = await _unitOfWork.LMSUserRepository.RetrieveCurrentUserIdAsync();
            }
            else
            {

                userId = userid;
            }


           var response =  await GetApproval(userId, classId);

            //var courseClass = await _unitOfWork.CourseClassRepository.GetByIdAsync(classId); 



            return Ok(response);
        }



        //DATATABLE-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost, Route("CourseClassesDataTable", Name = "GetCourseClassesDataTable")]
        //query : isTrainer (bool) -> retreive trainer specific
        //query : isLearner (bool) -> retreive learner spedific
        //query : courseId (int) -> retreive class for a particular course
        //query : lmsUserId(int) -> for a particular user
        public async Task<IActionResult> GetCourseClassesDataTable(

            [FromBody] DTParameterModel dTParameterModel,
            [FromQuery] int? courseId,
            [FromQuery] int? lmsUserId,
            [FromQuery] bool isTrainer=false , 
            [FromQuery] bool isLearner=false

            
            )
        {
            var response = new DTResponse<CourseClassTableData>();

            int userId;

            //RetrieveCurrentUserId() return the id of the current user if lmsuser is not supplied
            if (lmsUserId == null)
            {
                userId = await _unitOfWork.LMSUserRepository.RetrieveCurrentUserIdAsync();
            }

            //if lmsUserId is not null , check if user exists
            else
            {
                var user = await _unitOfWork.LMSUserRepository.GetByIdAsync((int)lmsUserId);

                if (user == null)
                {
                    var notFoundExp = new NotFoundException($"LMS User of Id {lmsUserId} does not exist");

                    throw notFoundExp;
                }
                //use lmsuserId supplied
                userId = (int)lmsUserId;
            }

            //if courseID is not null
            if (courseId != null)
            {
                //retreive course
                var course = await _unitOfWork.CourseRepository.GetByIdAsync((int)courseId);

                //course does not exist
                if (course == null)
                {

                    var notFoundExp = new NotFoundException($"Course of Id {courseId} does not exist");

                    throw notFoundExp;
                }
            }


            //retreive data
            response = await _unitOfWork.CourseClassRepository.GetCourseClassesDataTable(dTParameterModel, courseId, userId, isTrainer, isLearner);

            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(response);


            return Ok(responseJson);
        }


        //NON-API FUNCTIONS-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 


        [NonAction]
        public async Task SubmitEnrollmentRequest(LMSUser user, int classId)
        {

            //firstly retrieve class from classservice (check if class exists)
            var courseclass = await _unitOfWork.CourseClassRepository.GetByIdAsync(classId);
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
            else
            {
              
             

                throw new NotFoundException("Class does not exist"); ;
            }
            //Secondly use classenrollmentrecordservice to check eligibility 
            //if (!await new CoursesController(_unitOfWork).GetCourseEligiblity(courseclass.Course, ))
            //{
            //    var errorDict = new Dictionary<string, string>()
            //        {
            //            {"Class", $"Class of  Id {courseclass.Id} does not exist" }
            //        };

            //    var notFoundExp = new NotFoundException("Class does not exist", errorDict);

            //    throw notFoundExp;
            //}


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
                CourseClass = courseclass,
                LMSUser = user
                
            };
            if (user.Enrollments == null) {
                user.Enrollments = new List<ClassEnrollmentRecord>();
            }
            user.Enrollments.Add(record);
            await _unitOfWork.CompleteAsync();

        }


        [NonAction]
        public async Task<CourseClassesDTO> GetCourseClassDTOAsync(int courseClassId)
        {

            //check if class exists ; otherwise return not found
            //return courseclass
            var courseClass = await _unitOfWork.CourseClassRepository.GetByIdAsync(courseClassId, "Course,ClassTrainer,GradedQuiz");

            if (courseClass == null)
            {
                throw new NotFoundException($"Course class of id {courseClassId} does not exist");
            }

            return new CourseClassesDTO(courseClass);
        }

        [NonAction]
        public async Task<JsonResult> GetApproval(int userId,int classId)
        {

            //check if class exists ; otherwise return not found
            //return courseclass
            var user = await _unitOfWork.LMSUserRepository.GetByIdAsync(userId, "Enrollments");



            if (user == null)
            {
                throw new NotFoundException($"user of id {userId} does not exist");
            }


            if (user.Enrollments == null) {
                throw new NotFoundException($"Enrollment of id {userId} does not exist");
            }


            var enrollment = await _unitOfWork.ClassEnrollmentRecordRepository.GetAllAsync(filter:f=>f.LMSUser.Id==userId&&f.CourseClass.Id==classId); 
            



            if (enrollment == null) {
                throw new NotFoundException($"Enrollment of id {userId} does not exist");
            }


            return new JsonResult(new ClassEnrollmentRecordDTO(enrollment[0]));
        }
        





        [NonAction]
        public async Task<CourseClassesDTO> AssignTrainer(int trainerId,int courseClassId)
        {

            //check if class exists ; otherwise return not found
            //return courseclass
            var courseClass = await _unitOfWork.CourseClassRepository.GetByIdAsync(courseClassId, "Course,ClassTrainer");
            var trainer = await _unitOfWork.LMSUserRepository.GetByIdAsync(trainerId);
            if (courseClass == null)
            {
                throw new NotFoundException($"Course class of id {courseClassId} does not exist");
            }
            if (trainer == null)
            {
                throw new NotFoundException($"trainer not exist");
            }
            courseClass.ClassTrainer = trainer;
            await _unitOfWork.CompleteAsync();
            return new CourseClassesDTO(courseClass);
        }


        [NonAction]
        public async Task<CourseClassesDTO> WithdrawLearner(int learnerId, int courseClassId)
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
            var currentenrollment = await _unitOfWork.ClassEnrollmentRecordRepository.GetAllAsync(filter: f => f.CourseClass.Id == courseClassId && f.LMSUser.Id == learner.Id);


            if (currentenrollment == null || currentenrollment.Count ==0 ) {
                throw new NotFoundException($"Enrollment not exist");
            }
            await _unitOfWork.ClassEnrollmentRecordRepository.RemoveByIdAsync(currentenrollment[0].Id);
            await _unitOfWork.CompleteAsync();
            return new CourseClassesDTO(courseClass);
        }



        [NonAction]
        public async Task<CourseClassesDTO> AssignLearner(int learnerId, int courseClassId)
        {

            //check if class exists ; otherwise return not found
            //return courseclass
            var courseClass = await _unitOfWork.CourseClassRepository.GetByIdAsync(courseClassId, "Course");
            var learner = await _unitOfWork.LMSUserRepository.GetByIdAsync(learnerId, "ClassEnrollmentRecord");
            if (courseClass == null)
            {
                throw new NotFoundException($"Course class of id {courseClassId} does not exist");
            }
            //check if the learner exists  and throw if not exist
            if (learner == null)
            {
                throw new NotFoundException($"learner not exist");
            }

            var currentenrollment = await _unitOfWork.ClassEnrollmentRecordRepository.GetAllAsync(filter: f => f.CourseClass.Id == courseClassId && f.LMSUser.Id == learner.Id);


          

            //checks if there is existing enrolment and throw if exist
            if (currentenrollment.Count >0 && currentenrollment[0].IsEnrollled == true )
            {
                throw new NotFoundException($"Enrollment already exist and is enrolled");
            }

            //check if class is full
            if (await CheckIfClassFull(courseClassId)) {

                //is full
                throw new NotFoundException($"Class is full");

            }
            //has learner and no existing enrolment with the class, so lets add him or change to enrolled

            if ( currentenrollment.Count > 0 && currentenrollment[0].IsEnrollled == false)
            {
                currentenrollment[0].IsEnrollled = true;


            }
            else {
                //no such record so lets just add him
                await _unitOfWork.ClassEnrollmentRecordRepository.AddAsync(new ClassEnrollmentRecord
                {

                    LMSUser = learner,
                    IsEnrollled = true,
                    CourseClass = courseClass,
                    Course = courseClass.Course

                });

                //learner.Enrollments.Add(new ClassEnrollmentRecord
                //{

                //    LMSUser = learner,
                //    IsEnrollled = true,
                //    CourseClass = courseClass,
                //    Course = courseClass.Course

                //});
            }


            await _unitOfWork.CompleteAsync();
            return new CourseClassesDTO(courseClass);
        }


        [NonAction]
        public async Task<List<CourseClassesDTO>> GetCourseClassesDTOAsync(int? courseId)
        {
            List<CourseClassesDTO> results = new List<CourseClassesDTO>();
            List<CourseClass> courseClasses; 
            
            if (courseId!=null)
            {

                var course = await _unitOfWork.CourseRepository.GetByIdAsync((int)courseId); 

                if (course==null)
                {
                    throw new NotFoundException($"Course of id : {courseId} is not found"); 
                }


                courseClasses = await _unitOfWork.CourseClassRepository.GetAllAsync(cc=>cc.Course.Id==(int)courseId,null, "Course,ClassTrainer,GradedQuiz");
            }


            else
            {
                courseClasses = await _unitOfWork.CourseClassRepository.GetAllAsync(null,null, "Course,ClassTrainer,GradedQuiz");
            }
 

            return CourseClassesDTOList(courseClasses);

        }


        [NonAction]
        private List<CourseClassesDTO> CourseClassesDTOList(List<CourseClass> courseClasses)
        {
            List<CourseClassesDTO> results = new List<CourseClassesDTO>();

            if (courseClasses!= null)
            {
                foreach (CourseClass cc in courseClasses)

                {
                    results.Add(new CourseClassesDTO(cc));
                }
            }

            return results;
        }


        [NonAction]
        public async Task<CourseClass> GetCourseClassAsync(int id, string properties = "")
        {
            var courseClass = await _unitOfWork.CourseClassRepository.GetByIdAsync(id, properties);

            if (courseClass == null)
            {
                throw new NotFoundException($"Course Class of id {id} is not found");
            }
            return courseClass;
        }


        [NonAction]
        public async Task<bool> CheckIfClassFull(int courseClassId)
        {
            var courseClass = await _unitOfWork.CourseClassRepository.GetByIdAsync(courseClassId);
            //check if courseclass exist
            if (courseClass == null)
            {
                throw new NotFoundException($"Course Class of id {courseClassId} is not found");
            }

            //get the current class slots 
            var classSlots = courseClass.Slots;

            //get the current enrollments 
            var currentClassEnrolled = await _unitOfWork.ClassEnrollmentRecordRepository.GetAllAsync(filter: f => f.CourseClass.Id == courseClassId);


            if (currentClassEnrolled.Count >= classSlots)
            {
                return true;
            }
            else
            {

                return false;

            }

        }


    }
}