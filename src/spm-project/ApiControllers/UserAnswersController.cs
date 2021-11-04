using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPM_Project.CustomExceptions;
using SPM_Project.DTOs;
using SPM_Project.DTOs.RRModels;
using SPM_Project.EntityModels;
using SPM_Project.Extensions;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAnswersController : ControllerBase
    {


        public IUnitOfWork _unitOfWork;

        public QuizzesController _quizzesCon;

        public UsersController _usersCon;


        public UserAnswersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_courseClassesCon = new CourseClassesController(unitOfWork);
            _quizzesCon = new QuizzesController(unitOfWork);
            _usersCon = new UsersController(unitOfWork);

        }



        //get


        [HttpGet, Route("{id:int?}", Name = "GetUserAnswers")]
        public async Task<IActionResult> GetUserAnswerDTOs(int? id, [FromQuery] int? quizId)
        {

            if (id != null)
            {
                return Ok(new Response<UserAnswerDTO>(await GetUserAnswerDTOAsync((int)id, "QuizQuestion")));
            }

            else
            {


                return Ok(new Response<List<UserAnswerDTO>>(await GetUserAnswerDTOsAsync((int)quizId, "QuizQuestion")));


            }

        }



        //post












        //put








        //get user Answer---------------------------------------------------------------------------------------------------------------


        [NonAction]
        public async Task<UserAnswer> GetUserAnswerAsync(int id, string properties = "")
        {
            var userAns = await _unitOfWork.UserAnswerRepository.GetByIdAsync(id, properties);

            if (userAns == null)
            {
                throw new NotFoundException($"User Answer of id {id} is not found");
            }
            return userAns;
        }

        [NonAction]
        public async Task<UserAnswerDTO> GetUserAnswerDTOAsync(int id, string properties = "")
        {
            var chap = await GetUserAnswerAsync(id, properties);

            return new UserAnswerDTO(chap);
        }


        //get user Answers------------------------------------------------------------------------------------------------------------


        [NonAction]
        public async Task<List<UserAnswer>> GetUserAnswersAsync(int quizId, string properties = "")
        {
            //auto send badrequest exception 
            var cc = await _quizzesCon.GetQuizAsync(quizId, "");

            var userId = await _usersCon.GetCurrentUserId();

            return await _unitOfWork.UserAnswerRepository.GetAllAsync(filter: f => f.QuizQuestion.Id == cc.Id && f.User.Id == userId, includeProperties: properties);

        }



        [NonAction]
        public async Task<List<UserAnswerDTO>> GetUserAnswerDTOsAsync(int quizId, string properties = "")
        {
            var chaps = await GetUserAnswersAsync(quizId, properties);

            var result = new List<UserAnswerDTO>();

            foreach (var item in chaps)
            {
                result.Add(new UserAnswerDTO(item));
            }

            return result;

        }



        //convert UserAnswerDTO to Domain--------------------------------------------------------------------------------------------------------------------------------- 

        //public async Task<UserAnswer> ConvertDTOtoDomainAsync(UserAnswerDTO userDTO, bool isUpdate)
        //{
        //    if (isUpdate)
        //    {

        //    }
        //    else
        //    {

        //    }

        //}



        //public async Task<UserAnswer> UpdateConversion(UserAnswerDTO userDTO)
        //{

        //}



        //public async Task<UserAnswer> CreateConversion(UserAnswerDTO userDTO)
        //{
        //    var userAnswer = new UserAnswer()
        //    {
        //        QuizQuestion = await _unitOfWork.QuizQuestionRepository.GetByIdAsync(userDTO.QuestionId),
        //        User = await _unitOfWork.LMSUserRepository.GetByIdAsync(await _usersController.GetCurrentUserId()),

        //    };
        //}


        //check if answer is right & assign marks accordingly 
        public void  CheckAnswer(UserAnswer uAns)
        {

            var quizQuestion = uAns.QuizQuestion;
            uAns.Marks = 0; 

            if (quizQuestion.QuestionType=="McqQuestion")
            {
                var mcq = (McqQuestion)quizQuestion;

                var userAns = new List<int>().CommaSepStringToIntList(uAns.Answer);


                if (new HashSet<int>(mcq.GetAnswer()).SetEquals(userAns))
                {
                    uAns.Marks = quizQuestion.Marks; 
                }

                
            }
            else
            {
                var tf = (TFQuestion)quizQuestion;

                var userAns = bool.Parse(uAns.Answer);

                if (tf.GetAnswer() == userAns)
                {
                    uAns.Marks = quizQuestion.Marks;
                }

            }


        }

    }
}
