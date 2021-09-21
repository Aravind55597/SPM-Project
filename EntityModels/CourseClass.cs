using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class CourseClass
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationTimeStamp { get; set; }

        public DateTime UpdateTimeStamp { get; set; }


        public DateTime StartRegistration  { get; set; }

        public DateTime EndRegistration { get; set; }

        public DateTime StartClass  { get; set; }

        public DateTime EndClass { get; set; }


        //trainer of the class 
        public LMSUser ClassTrainer { get; set; }

        //course
        public Course Course { get; set; }

        public List<Chapter> Chapters { get; set; }

        public int MaxStudents { get; set; }

    }
}
