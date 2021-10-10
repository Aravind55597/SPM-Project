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
        Task<bool> HasEnrollmentRecord(LMSUser user, CourseClass courseclass);
    }
}
