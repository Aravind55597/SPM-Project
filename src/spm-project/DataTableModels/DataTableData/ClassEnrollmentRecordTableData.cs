using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.DataTableModels.DataTableData
{
    public class ClassEnrollmentRecordTableData : TableData
    {
        //class enrollment record id 
        public int Id { get; set; }

        //user id 
        public int UserId { get; set; }

        //name of the learner 
        public string LearnerName { get; set; }

        public string CourseName { get; set; }

        public string CourseClassName { get; set; }

        public bool IsAssigned { get; set; }

        public string RecordStatus { get; set; }

        public DateTime DateTimeRequested { get; set; }

    }

    public enum RecordStatus
    {
        Enrolled,
        RequestedEnrollment
    }




}
