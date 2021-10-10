using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class Course
    {

        public Course()
        {
            PassingPercentage = (decimal)0.85; 
        }


        public int Id { get; private set; }

        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreationTimestamp { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdateTimestamp { get; set; }


        public string Description { get; set; }

        public List<CourseClass> CourseClass { get; set; }

        public List<Course> PreRequisites { get; set; }


        public decimal PassingPercentage  { get; set; }
    }
}
