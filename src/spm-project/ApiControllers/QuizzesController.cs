using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPM_Project.DTOs;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {



        public IUnitOfWork _unitOfWork;

        public QuizzesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        //POST QUESTION-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //TODO UNIT TESTS 

        [HttpGet, Route("", Name = "GetCourseClasses")]
        public async Task<IActionResult> PostQuizDTOAPIAsync([FromBody] QuizDTO quiz)
        {

            //check the datetine of the class start if class has not started , throw badrequest 

            //check if chapter id exists, if not , throw not found 


            //check if otherwise , create the domain obejct and save changes 



            throw new  NotImplementedException(); 

        }





    }
}
