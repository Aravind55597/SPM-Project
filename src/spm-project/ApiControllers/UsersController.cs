using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetEngineersDataTable([FromBody]DTParameterModel dTParameterModel)
        {
            var response = await _unitOfWork.LMSUserRepository.GetEngineersDataTable(dTParameterModel);

            

            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            return Ok(responseJson);
      
        }



    }
}
