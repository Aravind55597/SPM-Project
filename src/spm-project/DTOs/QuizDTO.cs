using SPM_Project.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.DTOs
{

    //TODO UNIT TEST THIS CLASS
    public class QuizDTO
    {

        //default constructor for model binding 
        public QuizDTO()
        {

        }



        //contructor to create QuizDTO object
        public QuizDTO(Quiz  domain)
        {

        }


        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide a name for the Quiz")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide a description for the Quiz")]
        public string Description { get; set; }


        //check if it is graded 
        public bool IsGraded { get; set; }

        //relate to chapters if it is not a graded quiz 

        [Required(ErrorMessage = "Please provide an ID for the chapter")]
        public int Chapter { get; set; }

        //list of questions present 

        [Required(ErrorMessage = "Please provide atleast 1 question for the quiz")]
        public List<QuizQuestionDTO> Questions { get; set; }


        //time limit (in minutes)
        [Required(ErrorMessage = "Please provide a timelimit for the quiz")]
        public decimal TimeLimit { get; set; }





    }
}
