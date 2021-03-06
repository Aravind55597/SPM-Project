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
        
  
        public DateTime CreationTimestamp { get; set; }


        public DateTime UpdateTimestamp { get; set; }


        //check if assigned is true 
        public bool IsAssigned { get; set; }

        //check if the person is ernrolled 
        public bool IsEnrollled { get; set; }



        public CourseClass CourseClass { get; set; }


        public Course Course { get; set; }

        //to set the score for the class 
        public decimal PercentageScore { get; set; }

        public LMSUser LMSUser { get; set; }

    }
}
