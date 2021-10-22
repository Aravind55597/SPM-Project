using SPM_Project.EntityModels;
using SPM_Project.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SPM_Project.DTOs
{
    //TODO UNIT TEST THIS CLASS
    //https://www.intertech.com/unit-test-net-entity-validation/


    public class QuizQuestionDTO : IValidatableObject
    {



        public QuizQuestionDTO()
        {

        }

        //cosntructor to create QuizQuestionDTO object
        public QuizQuestionDTO(Quiz domain)
        {

        }

        public int Id { get; set; }

        [Url(ErrorMessage = "Invalid URL!")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Please provide the question")]
        public string Question { get; set; }

        [Required(ErrorMessage = "Please procvided the quesiton type")]
        public string QuestionType { get; set; }

        [Required(ErrorMessage = "Please provide answer for the question")]
        public string Answer { get; set; }

        public int? Marks { get; set; }

        //tf option

        [Required(ErrorMessage = "Please provide option")]
        public string TrueOption { get; set; }
        [Required(ErrorMessage = "Please provide option")]
        public string FalseOption { get; set; }

        //mcq option
        [Required(ErrorMessage = "Please provide option")]
        public string Option1 { get; set; }
        [Required(ErrorMessage = "Please provide option")]
        public string Option2 { get; set; }
        [Required(ErrorMessage = "Please provide option")]
        public string Option3 { get; set; }
        [Required(ErrorMessage = "Please provide option")]
        public string Option4 { get; set; }

        //check if multiselect or not
        public bool IsMultiSelect { get; set; }

        public QuizDTO Quiz { get; set; }


        //this method can't be tested directly as it is called by the framework to generate an inummerabe
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {


            //check if marks is provided
            if (Quiz.IsGraded)
            {
                if (Marks == null)
                {
                    yield return new ValidationResult(
                        $"Please provide marks for graded quizzes",
                        new[] { nameof(Marks) });
                }
            }



            //check if question type is properly provided
            if (!QuizQuestion.Discriminators.Contains(QuestionType))
            {
                yield return new ValidationResult(
                    $"Question types are  {string.Join(",", QuizQuestion.Discriminators)}",
                    new[] { nameof(QuestionType) });
            }

            //check if answers are fomatted correctly 
            if (QuizQuestion.Discriminators.Contains(QuestionType))
            {
                bool flag;
                switch (QuestionType)
                {
                    case "TFQuestion":

                        if (!Boolean.TryParse(Answer, out flag))
                        {
                            yield return new ValidationResult(
                                $"Please provide the proper format for the answer for a True/False Question",
                                new[] { nameof(Answer) });

                            //yield return new ValidationResult(
                            //    $"Ans hav"}",
                            //    new[] { nameof(QuestionType) });
                        }

                        break;

                    case "McqQuestion":

                        if (! new List<int>().CommaSepStringToIntListValidator(Answer))
                        {
                            yield return new ValidationResult(
                               $"Please provide the proper format for the answer for a MCQ Question ",
                                   new[] { nameof(Answer) });
                        }


                        break;
                }
            }
        }



    }
}