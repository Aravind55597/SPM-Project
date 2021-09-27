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

        //return classes for a particular course  

        //return  list of users for the class 

        //get id of trainer and remove trainer from class

        public Task<DTResponse<CourseClassTableData>> GetCourseClassesForTrainerDataTable(DTParameterModel dTParameterModel, int LMSId);
    }
}
