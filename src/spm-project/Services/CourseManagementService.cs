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
    public class CourseManagementService:ICourseManagementService
    {

        public IUnitOfWork _unitOfWork;

        public CourseManagementService(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork; 
        }


        //retrieve classes 

        //TODO REUSE this for admin 
        public async Task<DTResponse<CourseClassTableData>> GetCourseClassesDataTable(DTParameterModel dTParameterModel)
        {
            

            //RetrieveCurrentUserId() return the id of the current user ; for now return which is the test Trainer 
            //if null , return 1 
            int LMSUserId = await _unitOfWork.LMSUserRepository.RetrieveCurrentUserId() ?? 1; 

            //check role of the user 
            //var userRole = await _unitOfWork.LMSUserRepository.RetreiveCurrentUserRole() ?? "Trainer"; 


            var response = await _unitOfWork.CourseClassRepository.GetCourseClassesDataTable(dTParameterModel, LMSUserId);


            return response;
        }



        //retrieve courses 
        public async Task<DTResponse<CourseTableData>> GetCoursesDataTable(DTParameterModel dTParameterModel)
        {
            //simulate async code 
            //await Task.Delay(100); 

            var result = await _unitOfWork.CourseRepository.GetCoursesDataTable(dTParameterModel);


            return result; 


        }









    }
}
