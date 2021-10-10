using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class UserAnswer
    {

        public int Id { get; private set;  }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTimestamp { get; private set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedTimestamp { get; private set; }


        public QuizQuestion QuizQuestion { get; set; }

        public ApplicationUser User { get; set; }

        public string Answer { get; set; }

        //check if the ans is corect 
        public bool IsCorrect { get; set; }



    }
}
