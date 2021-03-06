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
        public Task<DTResponse<LMSUsersTableData>> GetEngineersDataTable(DTParameterModel dTParameterModel, bool isTrainer, bool isLearner, bool isEligible, int? classId);

     
        public Task<int> RetrieveCurrentUserIdAsync();

    

        public Task<List<string>> RetreiveUserRolesAsync(int LMSUserId);
        public List<ProgressTracker> GetCompletedProgressTracker(LMSUser user);
    }
}
