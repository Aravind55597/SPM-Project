using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class QuizQuestion
    {
        public int Id { get; set; }

        [Url(ErrorMessage = "Invalid URL!")]
        public string ImageUrl { get; set; }

        public string Question { get; set; }

        public string QuestionType { get; set; }

        //either array of integers for MCQ 
        //true false for 
        public string Answer { get; set; }

    }


    public class TFQuestion:QuizQuestion
    {
        public string TrueOption { get; set; }

        public string FalseOption { get; set; }

       
    }


    public class McqQuestion:QuizQuestion
    {
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }

        //check if multiselect or not 
        public bool IsMultiSelect { get; set; }

        //ans (serialise object and dump it in)
        /// <summary>
        /// eg.
        /// [
        /// 1,
        /// 2,
        /// 4
        /// ]
        /// </summary>
     

    }
}
