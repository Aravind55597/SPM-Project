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
    public class ClassManagementService : IClassManagementService
    {




        public IUnitOfWork _unitOfWork;

        public ClassManagementService(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }




        public async Task<DTResponse<CourseClassTableData>> GetCourseClassesDataTable(DTParameterModel dTParameterModel)
        {


            //RetrieveCurrentUserId() return the id of the current user ; 
            int LMSUserId = await _unitOfWork.LMSUserRepository.RetrieveCurrentUserIdAsync();


            //retrieve roles of the current user
            List<string> roles = await _unitOfWork.LMSUserRepository.RetreiveUserRolesAsync(LMSUserId);

            //currently a user has one role 
            var role = roles[0];


            var response = await _unitOfWork.CourseClassRepository.GetCourseClassesDataTable(dTParameterModel, LMSUserId, role);


            return response;
        }






    }
}
