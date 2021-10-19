using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPM_Project.CustomExceptions;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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



        //add crud 




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


    }
}

