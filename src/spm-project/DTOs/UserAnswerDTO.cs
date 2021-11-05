using SPM_Project.EntityModels;
using SPM_Project.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.DTOs
{
    public class UserAnswerDTO
    {




        public UserAnswerDTO()
        {

        }

        public UserAnswerDTO(UserAnswer domain)
        {
            Id = domain.Id;
            QuestionId = domain.QuizQuestion.Id;
            Answer = domain.Answer;
            IsCorrect = domain.IsCorrect;
            Marks = domain.Marks;

        }


        public int Id { get;  set; }

        [Required(ErrorMessage = "Please provide the Question Id")]
        public int QuestionId { get; set; }

        [Required(ErrorMessage = "Please provide an answer for the question")]
        public string Answer { get; set; }

        //check if the ans is corect 
        public bool IsCorrect { get; set; }

        public int Marks { get; set; }


        public bool IsAnswerFormated()
        {
            //try parsing as boolean 
            if (!bool.TryParse(Answer, out bool flag))
            {
                return false;
            }
            if (!new List<int>().CommaSepStringToIntListValidator(Answer))
            {
                return false;
            }

            return true;
        }





    }
}
