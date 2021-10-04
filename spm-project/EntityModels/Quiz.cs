using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class Quiz
    {

        public Quiz()
        {

        }
        public int Id { get; set; }


        public string Name { get; set; }

        public string Description { get; set; }


        public DateTime CreationTimeStamp { get; set; }


        public DateTime UpdateTimeStamp { get; set; }


        //check if it is graded 
        public bool IsGraded { get; set; }

        //relate to chapters if it is not a graded quiz 
        public Chapter Chapter { get; set; }



        //list of questions present 
        public List<QuizQuestion> Questions { get; set; }


        //time limit (in minutes)
        public decimal TimeLimit { get; set; }


    }





}
