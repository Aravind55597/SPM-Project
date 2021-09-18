using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableDataInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.PracticeDatatableModelAndController
{
    public class CustomerDT: IDTData
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int DT_RowId { get; set; }

        public string DT_RowClass { get; set; }

        public Dictionary<dynamic,dynamic> DT_RowData { get; set; }

        public Dictionary<dynamic, dynamic> DT_RowAttr { get; set; }
    }
}
