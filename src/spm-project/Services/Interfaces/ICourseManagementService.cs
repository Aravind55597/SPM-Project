using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Services.Interfaces
{
    public interface ICourseManagementService
    {


       
        public Task<DTResponse<CourseTableData>> GetCoursesDataTable(DTParameterModel dTParameterModel);
        bool GetCourseEligiblity(LMSUser user, Course course);
    }
}
