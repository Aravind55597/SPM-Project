using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Repositories.Interfaces
{
    public interface ILMSUserRepository: IGenericRepository<LMSUser>
    {

        //retrieve data to display all engineers present
        public Task<DTResponse<EngineersTableData>> GetEngineersDataTable(DTParameterModel dTParameterModel);

        //for now it will just return null
        public Task<int?> RetreiveCurrentUserId();

        //for now , it will just return null
        public Task<bool?> CheckCurrentUserRole(string Role); 

    }
}
