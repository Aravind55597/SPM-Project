using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPM_Project.DataTableModels;
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



        [HttpPost, Route("CourseClassesForTrainerDataTable", Name = "GetCourseClassesForTrainerDataTable")]
        public async Task<IActionResult> GetCourseClassesForTrainerDataTable([FromBody] DTParameterModel dTParameterModel)
        {
            var response = await _serviceManager.CourseManagementService.GetCourseClassesForTrainerDataTable(dTParameterModel);


            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            return Ok(responseJson);

        }





    }
}
