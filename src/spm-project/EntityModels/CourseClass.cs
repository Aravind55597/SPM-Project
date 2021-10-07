using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class CourseClass
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreationTimeStamp { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdateTimeStamp { get; set; }


        public DateTime StartRegistration  { get; set; }

        public DateTime EndRegistration { get; set; }

        public DateTime StartClass  { get; set; }

        public DateTime EndClass { get; set; }


        //trainer of the class 
        public LMSUser ClassTrainer { get; set; }

        //course
        public Course Course { get; set; }
        //course 
        public List<Chapter> Chapters { get; set; }


        //graded quiz 
        public Quiz GradedQuiz { get; set; }

        public int Slots { get; set; }


        public List<ClassEnrollmentRecord> ClassEnrollmentRecords { get; set; }

    }
}
