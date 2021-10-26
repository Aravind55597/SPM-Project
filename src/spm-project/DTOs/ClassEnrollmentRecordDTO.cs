using SPM_Project.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.DTOs
{
    public class ClassEnrollmentRecordDTO
    {


        public ClassEnrollmentRecordDTO()
        {

        }


        public ClassEnrollmentRecordDTO(ClassEnrollmentRecord domain)
        {
            Id = domain.Id;

            CompletionStatus = domain.CompletionStatus;

            IsAssigned = domain.IsAssigned;

            IsEnrollled = domain.IsEnrollled;

            if (domain.CourseClass!=null)
            {
                CourseClassId = domain.CourseClass.Id;
            }

            if (domain.Course != null)
            {
                CourseClassId = domain.Course.Id;
            }

            PercentageScore = domain.PercentageScore;


        }


        public int Id { get; private set; }

        public bool CompletionStatus { get; set; }

        //check if assigned is true 
        public bool IsAssigned { get; set; }

        //check if the person is ernrolled 
        public bool IsEnrollled { get; set; }

        public int CourseClassId { get; set; }

        public int CourseId { get; set; }

        public decimal PercentageScore { get; set; }

    }
}
