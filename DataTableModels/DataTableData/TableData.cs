using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.DataTableModels.DataTableData
{
    public class TableData
    {


        public int DT_RowId { get; set; }

        public string DT_RowClass { get; set; }

        public Dictionary<dynamic, dynamic> DT_RowData { get; set; }

        public Dictionary<dynamic, dynamic> DT_RowAttr { get; set; }


    }
}
