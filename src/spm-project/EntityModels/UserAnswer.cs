using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class UserAnswer
    {

        public int Id { get; set; }


        public DateTime CreateTimestamp { get; set; }

        public DateTime UpdatedTimestamp { get; set; }


        public QuizQuestion QuizQuestion { get; set; }

        public ApplicationUser User { get; set; }

        public string Answer { get; set; }

        //check if the ans is corect 
        public bool IsCorrect { get; set; }

    }
}
