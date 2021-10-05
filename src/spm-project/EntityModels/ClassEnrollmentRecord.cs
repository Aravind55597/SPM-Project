using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class ClassEnrollmentRecord
    {
        public int Id { get; set; }

        public bool CompletionStatus { get; set; }


        public bool Approved { get; set; }

        public CourseClass CourseClass { get; set; }

    }
}
