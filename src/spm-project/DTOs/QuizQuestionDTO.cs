using SPM_Project.EntityModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.DTOs
{


    //TODO UNIT TEST THIS CLASS
    public class QuizQuestionDTO : IValidatableObject
    {

        public int Id { get; private set; }


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
        public string TrueOption { get; set; }

        public string FalseOption { get; set; }

        //mcq option 
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }

        //check if multiselect or not
        public bool IsMultiSelect { get; set; }

        public QuizDTO Quiz { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {


            //check if marks is provided 
            if (Quiz.IsGraded)
            {
                if (Marks==null)
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

            //if (QuizQuestion.Discriminators.Contains(QuestionType))
            //{
            //    "TFQuestion","McqQuestion"

            //    switch (QuestionType)
            //    {
            //        case "TFQuestion":
            //            Console.WriteLine($"Measured value is {measurement}; too low.");
            //            break;

            //        case "McqQuestion":
            //            Console.WriteLine($"Measured value is {measurement}; too high.");
            //            break;

            //    }








                yield return new ValidationResult(
                    $"Question types are  {string.Join(",", QuizQuestion.Discriminators)}",
                    new[] { nameof(QuestionType) });

            }




        }



    }

