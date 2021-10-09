using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPM_Project.DataTableModels;
using SPM_Project.EntityModels;
using SPM_Project.Services.Interfaces;
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

        public IServiceManager _serviceManager;

        public CourseClassesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


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

            var response = await _serviceManager.ClassManagementService.GetCourseClassesDataTable(dTParameterModel, courseId , lmsUserId, isTrainer , isLearner);

            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            return Ok(responseJson);

        }

            


    }
}

