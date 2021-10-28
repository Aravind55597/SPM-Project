using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPM_Project.EntityModels
{
    public class CourseClass : IEntityWithId
    {


        //TODO UNIT TESTS
        public int Id { get; private set; }

        public string Name { get; set; }


        public DateTime CreationTimeStamp { get; set; }


        public DateTime UpdateTimeStamp { get; set; }


        public DateTime StartRegistration { get; set; }

        public DateTime EndRegistration { get; set; }

        public DateTime StartClass { get; set; }

        public DateTime EndClass { get; set; }


        //trainer of the class 
        public LMSUser ClassTrainer { get; set; }

        //course
        public Course Course { get; set; }
        //course 
        public List<Chapter> Chapters { get; set; }


        //graded quiz 
        public Quiz GradedQuiz { get; set; }

        //graded quiz 1d
        public int? GradedQuizId { get; set; }


        public int Slots { get; set; }


        public List<ClassEnrollmentRecord> ClassEnrollmentRecords { get; set; }





        public bool IsCourseClassModifiable()
        {
            if (StartClass > DateTime.Now)
            {
                return true;
            }

            return false;
        }


        public bool IsCourseClassregistrable()
        {
            if (DateTime.Now>= StartRegistration && DateTime.Now <=EndRegistration)
            {
                return true;
            }

            return false;
        }





    }



}
