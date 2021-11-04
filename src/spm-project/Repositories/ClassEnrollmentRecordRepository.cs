using Microsoft.EntityFrameworkCore;
using SPM_Project.Data;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels.DataTableRequest;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.EntityModels;
using SPM_Project.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace SPM_Project.Repositories
{
    public class ClassEnrollmentRecordRepository : GenericRepository<ClassEnrollmentRecord>, IClassEnrollmentRecordRepository
    {
        public ClassEnrollmentRecordRepository(ApplicationDbContext context) : base(context)
        {
        }

        //public async Task<ClassEnrollmentRecord> GetRecordBy(int id)
        //{
        //    var result = _context.ClassEnrollmentRecord.Include(cr=>cr.)
        //}

        public async Task<bool> hasEnrollmentRecord(LMSUser user, CourseClass courseclass)
        {
            var enrollments = _context.LMSUser.SelectMany(l => l.Enrollments);

            var record = enrollments.Where(e => e.CourseClass.Id == courseclass.Id).FirstOrDefault();
            if (record != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //--------------------------------------------TABLE FUNCTIONS------------------------------------------------------------------------------------------------------

        //generate IQueryable for manipulation by datatable

        private IQueryable<ClassEnrollmentRecordTableData> GetClassEnrollmentRecordsTableQueryable()
        {
            var queryable = _context
                .ClassEnrollmentRecord
                .Include(ce=>ce.Course)
                .Include(ce=>ce.CourseClass)
                .Select(ce => new ClassEnrollmentRecordTableData()
                {
                    Id = ce.Id,
                    UserId = ce.LMSUser.Id,
                    LearnerName = ce.LMSUser.Name,
                    IsAssigned = ce.IsAssigned,
                    RecordStatus= ce.IsEnrollled ? RecordStatus.Enrolled.ToString() :RecordStatus.RequestedEnrollment.ToString(),
                    DateTimeRequested = ce.CreationTimestamp,
                    CourseName=ce.Course.Name,
                    CourseClassName=ce.CourseClass.Name,
                    DT_RowId=ce.CourseClass.Id,
                    DT_RowData = new Dictionary<dynamic,dynamic>()
                    {
                        {"CourseClassId",ce.CourseClass.Id }
                    }
                });

            return queryable; 
        }


        private IQueryable<ClassEnrollmentRecordTableData> GlobalTableSearcher(IQueryable<ClassEnrollmentRecordTableData> queryable, DTRequestHandler<ClassEnrollmentRecordTableData> dtH)
        {
            //if search value is not empty
            if (!string.IsNullOrEmpty(dtH.SearchValue))
            {
                queryable = queryable.Where(m => m.LearnerName.Contains(dtH.SearchValue)
                                            || m.RecordStatus.Contains(dtH.SearchValue)  
                                            || m.CourseClassName.Contains(dtH.SearchValue)
                                            ||m.CourseName.Contains(dtH.SearchValue)
                                            );
            }

            return queryable;
        }

        public async Task<DTResponse<ClassEnrollmentRecordTableData>> GetClassEnrollmentRecordsDataTable(DTParameterModel dTParameterModel)
        {
            var dtH = new DTRequestHandler<ClassEnrollmentRecordTableData>(dTParameterModel);

            //Retrieve all userid + roleid pair that has either learnerRole or trainer role
            var queryable = GetClassEnrollmentRecordsTableQueryable();

            dtH.RecordsCounter(queryable);

            queryable = dtH.TableSorter(queryable);

            queryable = GlobalTableSearcher(queryable, dtH);

            queryable = dtH.TableFilterer(queryable);

            dtH.FilteredRecordsCounter(queryable);

       
           



            //skip 'start' records & Retrieve 'pagesize' records
            var data = await dtH.TablePager(queryable).ToListAsync();

            var dtResponse = new DTResponse<ClassEnrollmentRecordTableData>()
            {
                Draw = dtH.Draw,
                RecordsFiltered = dtH.RecordsTotal,
                RecordsTotal = dtH.RecordsFiltered,
                Data = data,
            };
            return dtResponse;
        }




    }
}