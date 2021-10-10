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

            //retreive all user 
            //retreive users in a particular class 

            //isEligible= true , isLearner=True , classid = 1
            // val()+"?isEligble=true&isLearner=True&classId=1" 
            )

        {
            //if class id is not null
            if (classId != null)
            {
                
                
                //retreive course 
                var courseClass = await _unitOfWork.CourseClassRepository.GetByIdAsync((int)classId);

                //class does not exist
                if (courseClass == null)
                {
                   
                    throw new NotFoundException("Class does not exist"); ;
                }


                if (isEligible)
                {
                    //cannot be aligible as trainer & learner for a classs
                    if (isTrainer && isLearner )
                    {
                        throw new BadRequestException("Can't be eligible both as Learner & a Trainer"); ;

                    }


                    //if user provide iseligible but no istrainer or islearner 
                    if (!isTrainer && !isLearner)
                    {

                        throw new BadRequestException("Need to provide either isLearner or isTrainer to check for eligibility");
                    }


                }

                //no eligibility is given 
                if (isTrainer || isLearner)
                {
                    throw new BadRequestException("Need to provide isEligible");
                }
                

            }
            else
            {
                if (isEligible)
                {
                    throw new BadRequestException("Need to provide classId to check for eligibility");
                }


                if ( (isTrainer || isLearner) || (isTrainer && isLearner) )
                {
                    throw new BadRequestException("isTrainer or IsLearner can only be used to check for eligibility for a class");
                }

            }

            //return the data 
            var response = await _unitOfWork.LMSUserRepository.GetEngineersDataTable(dTParameterModel, isTrainer, isLearner, isEligible, classId);

            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            return Ok(responseJson);

        }

        

    }
}
