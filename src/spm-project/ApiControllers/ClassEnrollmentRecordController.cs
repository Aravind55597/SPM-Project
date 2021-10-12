using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPM_Project.CustomExceptions;
using SPM_Project.DataTableModels;
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






        //DATATABLE-------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost, Route("EngineersDataTable", Name = "GetEngineersDataTable")]
        public async Task<IActionResult> GetClassEnrollmentRecordDataTable
        (

            [FromBody] DTParameterModel dTParameterModel,
            [FromQuery] int? classId,
            [FromQuery] bool isTrainer = false,
            [FromQuery] bool isLearner = false,
            [FromQuery] bool isEligible = false

            //Just class id -> retrieve engineers inside the class 
            //No class Id -> retreive all engineers 
            //isEligible -> if is eligible is used -> need classId AND isTrainer OR is Learner 

            //with class id -> retreive every one in the class 
            //iwhtout clas id -> retreive everyone
            //istrainer , islearn can only be used in conjuection with is eligible

            //retreive all user 
            //retreive users in a particular class 

            //isEligible= true , isLearner=True , classid = 1
            // val()+"?isEligble=true&isLearner=True&classId=1" 
            )

        {
            ////if class id is not null
            //if (classId != null)
            //{


            //    //retreive course 
            //    var courseClass = await _unitOfWork.CourseClassRepository.GetByIdAsync((int)classId);

            //    //class does not exist
            //    if (courseClass == null)
            //    {

            //        throw new NotFoundException("Class does not exist");
            //    }


            //    if (isEligible)
            //    {
            //        //cannot be aligible as trainer & learner for a classs
            //        if (isTrainer && isLearner)
            //        {
            //            throw new BadRequestException("Can't be eligible both as Learner & a Trainer"); ;

            //        }


            //        //if user provide iseligible but no istrainer or islearner 
            //        if (!isTrainer && !isLearner)
            //        {

            //            throw new BadRequestException("Need to provide either isLearner or isTrainer to check for eligibility");
            //        }


            //    }

            //    //no eligibility is given 
            //    if (isTrainer || isLearner)
            //    {
            //        throw new BadRequestException("Need to provide isEligible");
            //    }


            //}
            //else
            //{
            //    if (isEligible)
            //    {
            //        throw new BadRequestException("Need to provide classId to check for eligibility");
            //    }


            //    if ((isTrainer || isLearner) || (isTrainer && isLearner))
            //    {
            //        throw new BadRequestException("isTrainer or IsLearner can only be used to check for eligibility for a class");
            //    }

            //}

            ////return the data 
            //var response = await _unitOfWork.LMSUserRepository.GetEngineersDataTable(dTParameterModel, isTrainer, isLearner, isEligible, classId);

            //var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            //return Ok(responseJson);
            return Ok(); 
        }















        //BUSINESS LOGIC-------------------------------------------------------------------------------------------------------------------------------------

        [NonAction]
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
            if (!await new CoursesController(_unitOfWork).GetCourseEligiblity(user, courseclass.Course))
            {
                var errorDict = new Dictionary<string, string>()
                    {
                        {"Class", $"Class of  Id {courseclass.Id} does not exist" }
                    };

                var notFoundExp = new NotFoundException("Class does not exist", errorDict);

                throw notFoundExp;
            }


            //check if enrolled 

            if (await _unitOfWork.ClassEnrollmentRecordRepository.HasEnrollmentRecord(user, courseclass))
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








    }
}
