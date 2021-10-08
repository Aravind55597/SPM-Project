
using SPM_Project.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.DataTableModels.DataTableData
{
    public class LMSUsersTableData:TableData
    {

        //LMSUser ID 
        public int Id { get; set; }

        //name
        public string Name { get; set; }

        //Role
        public string Role { get; set; }

        //Department
        public string Department { get; set; }

        //DOB 
        public DateTime DOB { get; set; }



    }
}
