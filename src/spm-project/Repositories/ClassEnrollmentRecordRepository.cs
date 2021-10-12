using Microsoft.EntityFrameworkCore;
using SPM_Project.Data;
using SPM_Project.EntityModels;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Repositories
{
    public class ClassEnrollmentRecordRepository: GenericRepository<ClassEnrollmentRecord>, IClassEnrollmentRecordRepository
    {


        public ClassEnrollmentRecordRepository(ApplicationDbContext context) : base(context)
        {


        }

        //public async Task<ClassEnrollmentRecord> GetRecordBy(int id)
        //{
        //    var result = _context.ClassEnrollmentRecord.Include(cr=>cr.)
        //}

        public async Task<bool> HasEnrollmentRecord(LMSUser user, CourseClass courseclass)
        {
            var enrollments = _context.LMSUser.SelectMany(l => l.Enrollments);

            var record = enrollments.Where(e => e.CourseClass.Id == courseclass.Id).FirstOrDefault();
            if (record!=null)
            {
                return true;
            }
            else { 
                return false;
            }
        }



    }
}
