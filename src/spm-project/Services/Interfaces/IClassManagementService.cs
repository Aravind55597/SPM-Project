using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels.DataTableResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Services.Interfaces
{
    public interface IClassManagementService
    {


        public Task<DTResponse<CourseClassTableData>> GetCourseClassesDataTable(DTParameterModel dTParameterModel, int? courseId, int? lmsUserId , bool isTrainer, bool isLearner);



    }
}
