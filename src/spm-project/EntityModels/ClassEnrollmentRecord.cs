using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class ClassEnrollmentRecord: IEntityWithId
    {
        public int Id { get; private set; }

        public bool CompletionStatus { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreationTimestamp { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdateTimestamp { get; set; }


        public bool Approved { get; set; }

        public bool Withdrawm { get; set; }

        public CourseClass CourseClass { get; set; }

        //to set the score for the class 
        public decimal PercentageScore { get; set; }



    }
}
