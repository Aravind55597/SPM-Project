using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.Repositories.Interfaces;
using SPM_Project.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Services
{
    public class UserManagementService:IUserManagementService
    {


        public IUnitOfWork _unitOfWork;

        public UserManagementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        //retrieve all engineers (both learners & trainers) for datatable
        public async Task<DTResponse<EngineersTableData>> GetEngineersDataTable(DTParameterModel dTParameterModel)
        {



            var response = await _unitOfWork.LMSUserRepository.GetEngineersDataTable(dTParameterModel);

            return response;


        }






    }
}
