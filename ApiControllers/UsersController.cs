using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.Services.Interfaces;
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

        public IServiceManager _serviceManager; 

        public UsersController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager; 
        }



        [HttpPost, Route("Engineers", Name = "GetEngineersDataTable")]
        public async Task<DTResponse> GetEngineersDataTable([FromBody]DTParameterModel dTParameterModel)
        {
            var response = await _serviceManager.UserManagementService.GetEngineersDataTable(dTParameterModel);

            return response; 
        }



    }
}
