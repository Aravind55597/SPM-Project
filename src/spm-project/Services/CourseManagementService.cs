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
