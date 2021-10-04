using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationTimestamp { get; set; }

        public DateTime UpdateTimestamp { get; set; }


        public string Description { get; set; }

        public List<CourseClass> CourseClass { get; set; }

        public List<Course> PreRequisites { get; set; }
    }
}
