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
    public interface IClassEnrollmentRecordRepository : IGenericRepository<ClassEnrollmentRecord>
    {



        //create enrollment record -> when user sends request to enroll into a particular class 


        //remove learner -> pass id of the learner & remove the learner 
        Task<bool> hasEnrollmentRecord(LMSUser user, CourseClass courseclass);

        public Task<DTResponse<ClassEnrollmentRecordTableData>> GetClassEnrollmentRecordsDataTable(DTParameterModel dTParameterModel);



    }
}
