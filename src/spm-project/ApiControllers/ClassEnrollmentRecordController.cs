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




        //DATATABLE-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost, Route("ClassEnrollmentRecordsDataTable", Name = "GetClassEnrollmentRecordsDataTable")]
        public async Task<IActionResult> GetClassEnrollmentRecordsDataTable([FromBody] DTParameterModel dTParameterModel)
        {

            //return the data 
            var response = await _unitOfWork.ClassEnrollmentRecordRepository.GetClassEnrollmentRecordsDataTable(dTParameterModel);

            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            return Ok(responseJson);
        }





    }
}
