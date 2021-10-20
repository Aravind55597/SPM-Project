using Microsoft.EntityFrameworkCore;
using SPM_Project.Data;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels.DataTableRequest;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.EntityModels;
using SPM_Project.Repositories.Interfaces;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace SPM_Project.Repositories
{
    public class CourseClassRepository : GenericRepository<CourseClass>, ICourseClassRepository
    {
        public CourseClassRepository(ApplicationDbContext context) : base(context)
        {
        }



        //--------------------------------------------TABLE FUNCTIONS------------------------------------------------------------------------------------------------------

        //generate IQueryable for manipulation by datatable
        private IQueryable<CourseClassTableData> GetCourseClassTableQueryable(int? courseId, int userId, bool isTrainer = false, bool isLearner = false)
        {
            //queryable of course class

            var queryable = _context.CourseClass.AsQueryable(); 

            if (isTrainer)
            {
                queryable = queryable.Where(cc => cc.ClassTrainer.Id == userId); 

            }

            if (isLearner)
            {
                var enrollQueryable = _context.LMSUser.
                       Where(l => l.Id == userId).
                       SelectMany(l => l.Enrollments).
                       Where(e => e.IsEnrollled == true);

                queryable = enrollQueryable.Select(e => e.CourseClass); 

            }

            if (courseId!=null)
            {
                queryable = queryable.Where(cc=>cc.Course.Id== courseId); 
            }

            var result = queryable.Select(cc => new CourseClassTableData()
                    {
                        CourseName = cc.Course.Name,
                        ClassName = cc.Name,
                        StartDate = cc.StartClass,
                        EndDate = cc.EndClass,
                        TrainerName = cc.ClassTrainer.Name,
                        NumOfChapters = cc.Chapters.Count(),
                        NumOfStudents = cc.ClassEnrollmentRecords.Where(ce => ce.IsEnrollled).Count(),
                        Slots = cc.Slots,
                        DT_RowId = cc.Id
                    });;
            
            return result;

        }

        private IQueryable<CourseClassTableData> GlobalTableSearcher(IQueryable<CourseClassTableData> queryable, DTRequestHandler<CourseClassTableData> dtH)
        {


            //if search value is not empty
            if (!string.IsNullOrEmpty(dtH.SearchValue))
            {
                queryable = queryable.Where(m => m.CourseName.Contains(dtH.SearchValue)
                                            || m.ClassName.Contains(dtH.SearchValue)
                                            || m.TrainerName.Contains(dtH.SearchValue)
                                            );
            }

            return queryable;
        }

        public async Task<DTResponse<CourseClassTableData>> GetCourseClassesDataTable(DTParameterModel dTParameterModel, int? courseId,int userId ,  bool isTrainer=false, bool isLearner=false)
        {


            var dtH = new DTRequestHandler<CourseClassTableData>(dTParameterModel);

            var queryable = GetCourseClassTableQueryable(courseId, userId , isTrainer , isLearner);

            dtH.RecordsCounter(queryable);

            queryable = dtH.TableSorter(queryable);


            queryable = GlobalTableSearcher(queryable, dtH);


            queryable = dtH.TableFilterer(queryable);

            dtH.FilteredRecordsCounter(queryable);


            //skip 'start' records & Retrieve 'pagesize' records
            var data = await dtH.TablePager(queryable).ToListAsync();


            var dtResponse = new DTResponse<CourseClassTableData>()
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