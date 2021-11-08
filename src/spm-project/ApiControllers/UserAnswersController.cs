using Microsoft.AspNetCore.Mvc;
using SPM_Project.CustomExceptions;
using SPM_Project.DTOs;
using SPM_Project.DTOs.RRModels;
using SPM_Project.EntityModels;
using SPM_Project.Extensions;
using SPM_Project.Repositories.Interfaces;
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

        //TOO SIMPLE TO BE TESTED -> SKINNY CONTROLLER
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

        ////post
        //TOO SIMPLE TO BE TESTED -> SKINNY CONTROLLER
        [HttpPost, Route("", Name = "PostUserAnswers")]
        public async Task<IActionResult> PostUserAnswerDTOs([FromBody] List<UserAnswerDTO> uAnsDTOList)
        {
            //validate error
            var errorDict = ValidateInput(uAnsDTOList);


            if (errorDict.Count > 0)
            {
                throw new BadRequestException("Answers have been formatted wrongly", errorDict);
            }

            //loop through & convert to User answer
            List<UserAnswer> userAnswers = await ConvertDTOtoDomainAsync(uAnsDTOList, false);

            //loop through & set the marks
            CheckAnswerList(userAnswers);

            //save changes 
            await _unitOfWork.UserAnswerRepository.AddRangeAsync(userAnswers);

            await _unitOfWork.CompleteAsync();

            //pass back the ans to the ui to show the marks
            //return Ok(new Response<List<UserAnswerDTO>>( ConvertDomaintoDTO(userAnswers), "QuizQuestion")));

            return Ok(new Response<List<UserAnswerDTO>>(ConvertDomaintoDTO(userAnswers)));
        }
        //TOO SIMPLE TO BE TESTED -> SKINNY CONTROLLER

        [HttpPut, Route("", Name = "UpdateUserAnswers")]
        public async Task<IActionResult> UpdateUserAnswerDTOs([FromBody] List<UserAnswerDTO> uAnsDTOList)
        {
            //validate error
            var errorDict = ValidateInput(uAnsDTOList);


            if (errorDict.Count > 0)
            {
                throw new BadRequestException("Answers have been formatted wrongly", errorDict);
            }

            //loop through & convert to User answer
            List<UserAnswer> userAnswers = await ConvertDTOtoDomainAsync(uAnsDTOList, true);

            //loop through & set the marks
            CheckAnswerList(userAnswers);

            //save changes 

            await _unitOfWork.CompleteAsync();

            //pass back the ans to the ui to show the marks
            //return Ok(new Response<List<UserAnswerDTO>>( ConvertDomaintoDTO(userAnswers), "QuizQuestion")));

            return Ok(new Response<List<UserAnswerDTO>>(ConvertDomaintoDTO(userAnswers)));
        }



        //get user Answer---------------------------------------------------------------------------------------------------------------

        [NonAction]
        //TEST THIS -> SANITY CHECK 
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
        //TEST THIS -> SEEMS SIMPLE BUT RELIES ON USER ANSWER DTO CONSTRUCTOR (BETTER TO TEST IT JUST IN CASE)
        public async Task<UserAnswerDTO> GetUserAnswerDTOAsync(int id, string properties = "")
        {
            var chap = await GetUserAnswerAsync(id, properties);

            return new UserAnswerDTO(chap);
        }

        //get user Answers------------------------------------------------------------------------------------------------------------

        [NonAction]
 
        //DIIFICULT TO TEST THIS
        public async Task<List<UserAnswer>> GetUserAnswersAsync(int quizId, string properties = "")
        {
           
            //auto send badrequest exception
            var quiz = await _quizzesCon.GetQuizAsync(quizId, "Questions");

            var quesIdList = quiz.Questions.Select(q => q.Id).ToList();

            var userId = await _usersCon.GetCurrentUserId();
            
             return  await RetreiveUserAnsList(properties, quesIdList, userId);
            

            //return await _unitOfWork.QuizQuestionRepository.GetAllAsync(filter: f => f..Id == cc.Id && f.User.Id == userId, includeProperties: properties);

        }

        private async Task<List<UserAnswer>> RetreiveUserAnsList(string properties, List<int> quesIdList, int userId)
        {
            var result = new List<UserAnswer>();
            foreach (var item in quesIdList)
            {

                var uAns = await _unitOfWork.UserAnswerRepository.GetAllAsync(filter: f => f.QuizQuestion.Id == item && f.User.Id == userId, includeProperties: properties);
                result.Add(uAns[0]);
            }
            return result; 
        }


        [NonAction]
        //TOO SIMPLE TO BE TESTED ; JUST A LOOP
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
        [NonAction]
        //TOO SIMPLE TO BE TESTED ; JUST A LOOP
        public async Task<List<UserAnswer>> ConvertDTOtoDomainAsync(List<UserAnswerDTO> userDTOs, bool isUpdate)
        {
            //LOOPS ARE NOT EXTRACTED AS A SEPARATE METHOD -> TOO SIMPLE & ONLY CREATES UNNECESSARY FUNCTION 
            
            List<UserAnswer> result = new List<UserAnswer>();

            foreach (var item in userDTOs)
            {
                if (isUpdate)
                {
                    result.Add(await UpdateConversionAsync(item));
                    continue;
                }
                result.Add(await CreateConversionAsync(item));
            }

            return result; 

        }


        //TOO SIMPLE TO BE TESTED
        [NonAction]
        public async Task<UserAnswer> UpdateConversionAsync(UserAnswerDTO userDTO)
        {

            var userAns = await GetUserAnswerAsync(userDTO.Id, "QuizQuestion");

            userAns.Answer = userDTO.Answer;

            return userAns; 

        }

        //TEST THIS 
        [NonAction]
        public async Task<UserAnswer> CreateConversionAsync(UserAnswerDTO userDTO)
        {
            var userAnswer = new UserAnswer()
            {
                QuizQuestion = await _quizzesCon.GetQuizQuestionAsync(userDTO.QuestionId),
                User =  await _usersCon.GetLMSUserAsync(await _usersCon.GetCurrentUserId()),
                Answer=userDTO.Answer
            };

            return userAnswer;
        }



        //convert list of userAnswer back to DTO---------------------------------------------------------------------------------------------------------------------------------------------
        //TOO SIMPLE TO TEST
        [NonAction]
        public List<UserAnswerDTO> ConvertDomaintoDTO(List<UserAnswer> userAnsList)
        {
            var result = new List<UserAnswerDTO>();
            foreach (var item in userAnsList)
            {
                result.Add(new UserAnswerDTO(item)); 
            }
            return result; 
        }


        //autograding---------------------------------------------------------------------------------------------------------------------------------

        //NO NEED TO TEST THIS (JUST A LOOP)
        [NonAction]
        public void CheckAnswerList(List<UserAnswer> uAnsList)
        {
            foreach (var item in uAnsList)
            {
                CheckAnswer(item); 
            }
        }

        //TEST THIS
        [NonAction]
        public void CheckAnswer(UserAnswer uAns)
        {
            var quizQuestion = uAns.QuizQuestion;
            uAns.Marks = 0;

            if (quizQuestion.QuestionType == "McqQuestion")
            {
                var mcq = (McqQuestion)quizQuestion;

                var userAns = new List<int>().CommaSepStringToIntList(uAns.Answer);

                if (new HashSet<int>(mcq.GetAnswer()).SetEquals(userAns))
                {
                    uAns.Marks = quizQuestion.Marks;
                    uAns.IsCorrect = true;
                }
            }
            else
            {
                var tf = (TFQuestion)quizQuestion;

                var userAns = bool.Parse(uAns.Answer);

                if (tf.GetAnswer() == userAns)
                {
                    uAns.Marks = quizQuestion.Marks;
                    uAns.IsCorrect = true;
                }
            }
        }

        [NonAction]
        //NO NEED TO TEST THIS -> TOO SIMPLE
        public Dictionary<string,string> ValidateInput(List<UserAnswerDTO> uAnsList)
        {
            var errorDict = new Dictionary<string, string>();

            for (int i = 0; i < uAnsList.Count; i++)
            {
                if (!uAnsList[i].IsAnswerFormated())
                {
                    errorDict.Add($"UserAnswerDTO[{i}]","Ans is not formatted correctly"); 
                }
            }

            return errorDict; 
        }
    }
}