using SPM_Project.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class UserAnswer : IEntityWithId
    {

        public int Id { get; private set;  }


        public DateTime CreationTimestamp { get; set; }


        public DateTime UpdateTimestamp { get; set; }

        public QuizQuestion QuizQuestion { get; set; }

        public LMSUser User { get; set; }

        public string Answer { get; set; }

        //check if the ans is corect 
        public bool IsCorrect { get; set; }


        public int Marks { get; set; }




        
    }
}
