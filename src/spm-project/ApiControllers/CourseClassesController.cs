using Microsoft.AspNetCore.Mvc;
using SPM_Project.CustomExceptions;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.DTOs;
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
            //if (id!=null)
            //{
            //    var result = await RetreiveCourseClasses(id);
            //}

            //var courseController = new CoursesController(_unitOfWork);

            //var course = courseController.RetreiveCourses(courseId);

            //var result = await RetreiveCourseClasses(id);

            //check if id is null

            //if id is null , check if courseID is null
            //if courseid is nuil -> return all classes
            //if course id is not null , check if course exists
            //if course is null , return 404
            //if course is not null , retreive classes for the course

            //if id is not null ,
            //check if class exists
            //if class does not exsits -> return NotFound
            //if class exists -> return 1 class

            throw new NotImplementedException();
        }

        
        



        //DATATABLE-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost, Route("CourseClassesDataTable", Name = "GetCourseClassesDataTable")]
        //query : isTrainer (bool) -> retreive trainer specific
        //query : isLearner (bool) -> retreive learner spedific
        //query : courseId (int) -> retreive class for a particular course
        //query : lmsUserId(int) -> for a particular user
        public async Task<IActionResult> GetCourseClassesDataTable(

            [FromBody] DTParameterModel dTParameterModel , 
            [FromQuery] int? courseId ,
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
        public async Task<CourseClassesDTO> GetCourseClassDTOAsync(int courseClassId)
        {

            //check if class exists ; otherwise return not found
            //return courseclass
            var courseClass = await _unitOfWork.CourseClassRepository.GetByIdAsync(courseClassId, "Course ClassTrainer");

            if (courseClass == null)
            {
                throw new NotFoundException($"Course class of id { courseClassId} does not exist");
            }

            return new CourseClassesDTO(courseClass);
        }





        [NonAction]
        public async Task<List<CourseClassesDTO>> GetCourseClassesDTOAsync(int? courseId)
        {
            List<CourseClassesDTO> results = new List<CourseClassesDTO>();
            List<CourseClass> courseClasses; 
            
            if (courseId!=null)
            {
                courseClasses = await _unitOfWork.CourseClassRepository.GetAllAsync(cc=>cc.Id==(int)courseId,null, "Course ClassTrainer");

               
            }
            else
            {
                courseClasses = await _unitOfWork.CourseClassRepository.GetAllAsync(null,null,"Course ClassTrainer");
            }

            TestCourseClassCreator(); 

            return CourseClassesDTOList(courseClasses);

        }


        [NonAction]
        private List<CourseClassesDTO> CourseClassesDTOList(List<CourseClass> courseClasses)
        {
            List<CourseClassesDTO> results = new List<CourseClassesDTO>();

            foreach (CourseClass cc in courseClasses)

            {
                results.Add(new CourseClassesDTO(cc));
            }

            return results;
        }



        ////create list of test course class 
        //public List<CourseClass> TestCourseClassListCreator()
        //{

        //    List<CourseClass> courseClassesList = new List<CourseClass>();


        //    for (int i = 0; i < 10; i++)
        //    {
        //        CourseClass cc = new TestCourseClassCreator();
        //        courseClassesList.Add();
        //    }

        //    return courseClassesList;
        //}

        public CourseClass ReturnCC()
        {
            return TestCourseClassCreator(); 
        }

        public List<CourseClass> TestCourseClassList()
        {
            List<CourseClass> courseClassesList = new List<CourseClass>();

            for (int i = 0; i < 10; i++)
            {
                courseClassesList.Add(TestCourseClassCreator()); 
            }

            return courseClassesList;
        }





        //create 
        public CourseClass TestCourseClassCreator()
        {

            Random rnd = new Random();
            int id = rnd.Next(1, 50);

            var courseClass = new CourseClass()
            {


                Name = $"Test Course Class {id}",
                StartRegistration = DateTime.Now,
                EndRegistration = DateTime.Now,
                StartClass = DateTime.Now,
                EndClass = DateTime.Now,
                ClassTrainer = new LMSUser()
                {
                    Name = $"Test Trainer {id}",
                    Department = Department.Human_Resource,
                    DOB = DateTime.Now,

                },
                Course = new Course()
                {
                    Name = $"Test Course {id}",
                    Description = "Test Description",
                    PassingPercentage = (decimal)0.85
                },
                Slots = 30

            };

            //set id of the courseClass 
            typeof(CourseClass).GetProperty(nameof(courseClass.Id)).SetValue(courseClass, id);

            //set id of classtrainer 
            typeof(LMSUser).GetProperty(nameof(courseClass.ClassTrainer.Id)).SetValue(courseClass.ClassTrainer, id);

            //set id of course 
            typeof(Course).GetProperty(nameof(courseClass.Course.Id)).SetValue(courseClass.Course, id);

            return courseClass;

        }







    }
}