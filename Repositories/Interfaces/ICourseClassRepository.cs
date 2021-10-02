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
    public interface ICourseClassRepository:IGenericRepository<CourseClass>
    {

        
        public Task<DTResponse<CourseClassTableData>> GetCourseClassesForTrainerDataTable(DTParameterModel dTParameterModel , int LMSId);

        public Task<DTResponse<CourseClassTableData>> GetCourseClassesDataTable(DTParameterModel dTParameterModel);



    }
}
