using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Services.Interfaces
{
    public interface IUserManagementService
    {
        public Task<DTResponse> GetEngineersDataTable(DTParameterModel dTParameterModel); 


    }
}
