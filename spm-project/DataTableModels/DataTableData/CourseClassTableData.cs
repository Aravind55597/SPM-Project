
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.DataTableModels.DataTableData
{
    public class CourseClassTableData : TableData
    {
        public string CourseName { get; set; }

        public string ClassName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int NumOfChapters { get; set; }

        public int NumOfStudents { get; set; }




    }
}
