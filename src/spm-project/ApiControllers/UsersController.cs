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
            [FromQuery] bool isLearner = false,
            [FromQuery] bool isEligible = false 

            //Just class id -> retrieve engineers inside the class 
            //No class Id -> retreive all engineers 
            //isEligible -> if is eligible is used -> need classId AND isTrainer OR is Learner 

            //with class id -> retreive every one in the class 
            //iwhtout clas id -> retreive everyone
            //istrainer , islearn can only be used in conjuection with is eligible
            )

        {

            var errorTextBadRequest = "When checking eligibility , the parameters you have to provide are : isEligible , classId , isTrainer OR isLearner";

            var errorTextNotFound = "Class does not exist"; 

            if (isEligible)
            {
                if (  (classId == null ) ||  (!isLearner && !isTrainer )    ||   (isLearner && isTrainer)  )
                {
                    throw new BadRequestException(errorTextBadRequest);
                }
            }
            else
            {
                if (isLearner || isTrainer)
                {
                    throw new BadRequestException(errorTextBadRequest);
                }

            }

            if (classId != null)
            {
                //retreive course 
                var courseClass = await _unitOfWork.CourseClassRepository.GetByIdAsync((int)classId);

                //class does not exist
                if (courseClass == null)
                {
                    throw new NotFoundException(errorTextNotFound); ;
                }
            }




            //return the data 
            var response = await _unitOfWork.LMSUserRepository.GetEngineersDataTable(dTParameterModel, isTrainer, isLearner, isEligible, classId);

            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            return Ok(responseJson);

        }



        public virtual  async Task<int> GetCurrentUserId() {

            return await _unitOfWork.LMSUserRepository.RetrieveCurrentUserIdAsync(); 

        } 



    }
}
