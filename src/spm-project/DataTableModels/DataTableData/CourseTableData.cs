using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.DataTableModels.DataTableData
{
    public class CourseTableData :TableData
    {

        public string CourseName { get; set; }

        public int NumberOfClasses { get; set; }

        public DateTime CreatedDate { get; set; }


        public DateTime UpdatedDate { get; set; }



    }
}
