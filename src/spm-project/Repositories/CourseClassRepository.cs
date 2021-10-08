using Microsoft.EntityFrameworkCore;
using SPM_Project.Data;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
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
        private IQueryable<CourseClassTableData> GetCourseClassTableQueryable(int lmsId, string role)
        {
            //queryable of course class
   

            switch (role)
            {
                case "Trainer":

                    var queryableT = _context.CourseClass.Where(cc => cc.ClassTrainer.Id == lmsId).
                    Select(cc => new CourseClassTableData()
                    {
                        CourseName = cc.Course.Name,
                        ClassName = cc.Name,
                        StartDate = cc.StartClass,
                        EndDate = cc.EndClass,
                        TrainerName = _context.Users.Where(u => u.LMSUser.Id == cc.ClassTrainer.Id).Select(u => u.Name).FirstOrDefault(),
                        NumOfChapters = cc.Chapters.Count(),
                        NumOfStudents = cc.ClassEnrollmentRecords.Where(ce => ce.Approved).Count(),
                        Slots = cc.Slots,
                        DT_RowId = cc.Id
                    });

                    return queryableT; 


                case "Learner":
                    var enrollQueryable = _context.LMSUser.
                        Where(l => l.Id == lmsId).
                        SelectMany(l=>l.Enrollments).
                        Where(e=>e.Approved==true);


                    var queryableL = enrollQueryable.Select(e=>e.CourseClass).
                    Select(cc => new CourseClassTableData()
                    {
                        CourseName = cc.Course.Name,
                        ClassName = cc.Name,
                        StartDate = cc.StartClass,
                        EndDate = cc.EndClass,
                        TrainerName = _context.Users.Where(u => u.LMSUser.Id == cc.ClassTrainer.Id).Select(u => u.Name).FirstOrDefault(),
                        NumOfChapters = cc.Chapters.Count(),
                        NumOfStudents = cc.ClassEnrollmentRecords.Where(ce => ce.Approved).Count(),
                        Slots = cc.Slots,
                        DT_RowId = cc.Id
                    });

                    return queryableL;

                default:
                    var queryableA = _context.CourseClass.
                    Select(cc => new CourseClassTableData()
                    {
                        CourseName = cc.Course.Name,
                        ClassName = cc.Name,
                        StartDate = cc.StartClass,
                        EndDate = cc.EndClass,
                        TrainerName = _context.Users.Where(u => u.LMSUser.Id == cc.ClassTrainer.Id).Select(u => u.Name).FirstOrDefault(),
                        NumOfChapters = cc.Chapters.Count(),
                        NumOfStudents = cc.ClassEnrollmentRecords.Where(ce => ce.Approved).Count(),
                        Slots = cc.Slots,
                        DT_RowId = cc.Id
                    });
                    return queryableA;

            }

        }

        public async Task<DTResponse<CourseClassTableData>> GetCourseClassesDataTable(DTParameterModel dTParameterModel, int lmsId , string role )
        {
            //LMSId is validated to be a Trainer Id in the service layer

            var draw = dTParameterModel.Draw;
            var start = dTParameterModel.Start;
            var length = dTParameterModel.Start;
            var sortColumn = dTParameterModel.Columns[dTParameterModel.Order[0].Column].Name;
            var sortColumnDirection = dTParameterModel.Order[0].Dir;
            var searchValue = dTParameterModel.Search.Value;
            int pageSize = dTParameterModel.Length;

            //number of records to be skipped
            int skip = dTParameterModel.Start;
            int recordsTotal = 0;
            int recordsFiltered = 0;

            var queryable = GetCourseClassTableQueryable(lmsId , role);

            recordsTotal = queryable.Count();

            //if sortcolumn and sort colum direction are not empty
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                queryable = queryable.OrderBy(sortColumn + " " + sortColumnDirection);
            }

            //if search value is not empty
            if (!string.IsNullOrEmpty(searchValue))
            {
                queryable = queryable.Where(m => m.CourseName.Contains(searchValue)
                                            || m.ClassName.Contains(searchValue)

                                            );
            }

            recordsFiltered = queryable.Count();

            var data = await queryable.Skip(skip).Take(pageSize).ToListAsync();

            //repeated
            var dtResponse = new DTResponse<CourseClassTableData>()
            {
                Draw = draw,
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal,
                Data = data,
            };

            return dtResponse;
        }
    }
}