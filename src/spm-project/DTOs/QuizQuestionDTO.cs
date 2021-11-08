using SPM_Project.EntityModels;
using SPM_Project.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SPM_Project.DTOs
{

    //https://www.intertech.com/unit-test-net-entity-validation/

    public class QuizQuestionDTO
    
    {
        public QuizQuestionDTO()
        {
        }

        //cosntructor to create QuizQuestionDTO object
        public QuizQuestionDTO(TFQuestion tFQuestion)
        {
            Id = tFQuestion.Id;
            ImageUrl = tFQuestion.ImageUrl;
            Question = tFQuestion.Question;
            QuestionType = tFQuestion.QuestionType;
            Answer = tFQuestion.Answer;
            Marks = tFQuestion.Marks; 
            TrueOption = tFQuestion.TrueOption;
            FalseOption = tFQuestion.FalseOption; 
        }

        //contructor to create QuizQuestionDTO object
        public QuizQuestionDTO(McqQuestion mcqQuestion)
        {
            Id = mcqQuestion.Id;
            ImageUrl = mcqQuestion.ImageUrl;
            Question = mcqQuestion.Question;
            QuestionType = mcqQuestion.QuestionType;
            Answer = mcqQuestion.Answer;
            Marks = mcqQuestion.Marks;
            IsMultiSelect = mcqQuestion.IsMultiSelect;
            Option1 = mcqQuestion.Option1;
            Option2 = mcqQuestion.Option2;
            Option3 = mcqQuestion.Option3;
            Option4 = mcqQuestion.Option4;
        }

        //McqQuestion mcqQuestion = null

        public int Id { get; private set; }

        [Url(ErrorMessage = "Invalid URL!")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Please provide the question")]
        public string Question { get; set; }

        [Required(ErrorMessage = "Please provide the question type")]
        public string QuestionType { get; set; }

        [Required(ErrorMessage = "Please provide answer for the question")]
        public string Answer { get; set; }

        [Required(ErrorMessage = "Please provide the marks for the questions")]
        public int Marks { get; set; }

        //tf option
        public string TrueOption { get; set; }

        public string FalseOption { get; set; }

        //mcq option
        public string Option1 { get; set; }

        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }

        //check if multiselect or not
        public bool IsMultiSelect { get; set; }




        public bool IsQuestionTypeProvided()
        {
            if (!QuizQuestion.Discriminators.Contains(QuestionType))
            {
                return false; 
            }
            return true; 
        }

        public bool IsAnswerFormatted()
        {
            if (QuizQuestion.Discriminators.Contains(QuestionType))
            {
                switch (QuestionType)
                {
                    case "TFQuestion":

                        if (!Boolean.TryParse(Answer, out bool flag))
                        {
                            return false;
                        }
                        break;

                    case "McqQuestion":

                        if (!new List<int>().CommaSepStringToIntListValidator(Answer))
                        {
                            return false;
                        }
                        else
                        {
                            var ans = new List<int>().CommaSepStringToIntList(Answer);

                            if (ans.Any(a => a < 1 || a > 4))
                            {
                                return false;
                            }

                            if (IsMultiSelect)
                            {
                                if ((ans.Count <= 1 || ans.Count > 4))
                                {
                                    return false;
                                }

                            }

                            else
                            {
                                if (ans.Count > 1 || ans.Count < 1)
                                {
                                    return false;
                                }
                            }

                        }
                        break;

                }
                return true; 
            }
            return false; 
        }




    
    }
}