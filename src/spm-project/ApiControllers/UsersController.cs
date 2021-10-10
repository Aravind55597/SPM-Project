using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPM_Project.CustomExceptions;
using SPM_Project.DataTableModels;
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
    public class UsersController : ControllerBase
    {


        public IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

















        [HttpPost, Route("EngineersDataTable", Name = "GetEngineersDataTable")]
        public async Task<IActionResult> GetEngineersDataTable
            (
            
            [FromBody]DTParameterModel dTParameterModel ,
            [FromQuery] int? classId,
            [FromQuery] bool isTrainer = false,
            [FromQuery] bool isLearner = false

            )


        {

            //if courseID is not null
            if (classId != null)
            {
                //retreive course 
                var course = await _unitOfWork.CourseClassRepository.GetByIdAsync((int)classId);

                //course does not exist
                if (course == null)
                {
                    var errorDict = new Dictionary<string, string>()
                    {
                        {"ClassId", $"Course of Id {classId} does not exist" }
                    };

                    var notFoundExp = new NotFoundException("Class does not exist", errorDict);

                    throw notFoundExp;
                }
            }


            var response = await _unitOfWork.LMSUserRepository.GetEngineersDataTable(dTParameterModel , isTrainer , isLearner, classId);

            

            //var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            return Ok(response);
      
        }



    }
}
