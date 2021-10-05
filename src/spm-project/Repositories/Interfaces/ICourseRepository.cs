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
    public interface ICourseRepository: IGenericRepository<Course>
    {

        public Task<DTResponse<CourseTableData>> GetCoursesDataTable(DTParameterModel dTParameterModel); 


    }

}
