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
          

            if (id!=null && courseId!= null)
            {
                throw new BadRequestException("You can either query a Class of a praticular Id OR all classes OR retreive classes for a course"); 
            }

            if (id!=null)
            {
                return Ok(new Response<CourseClassesDTO>(await GetCourseClassDTOAsync(id.GetValueOrDefault()))); 
            }

            return Ok(new Response<List<CourseClassesDTO>>(await GetCourseClassesDTOAsync(courseId))); 
  
           
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
            var courseClass = await _unitOfWork.CourseClassRepository.GetByIdAsync(courseClassId, "Course,ClassTrainer");

            if (courseClass == null)
            {
                throw new NotFoundException($"Course class of id {courseClassId} does not exist");
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

                var course = await _unitOfWork.CourseRepository.GetByIdAsync((int)courseId); 

                if (course==null)
                {
                    throw new NotFoundException($"Course of id : {courseId} is not found"); 
                }


                courseClasses = await _unitOfWork.CourseClassRepository.GetAllAsync(cc=>cc.Course.Id==(int)courseId,null, "Course,ClassTrainer");
            }


            else
            {
                courseClasses = await _unitOfWork.CourseClassRepository.GetAllAsync(null,null,"Course,ClassTrainer");
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







    }
}