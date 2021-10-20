using SPM_Project.Data;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.EntityModels;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using SPM_Project.DataTableModels.DataTableRequest;

namespace SPM_Project.Repositories
{
    public class CourseRepository :GenericRepository<Course>, ICourseRepository
    {

        public CourseRepository(ApplicationDbContext context) : base(context)
        {
            //TODO Retrieve course based on 

        }

        //public List<Course> GetCoursePreRe(int  id )
        //{


        //    var _course = _context.Course.Where(c => c.Id == course.Id).First();

        //    //get the course prereq for current course
        //    return _course.PreRequisites;

        //}


       
        //--------------------------------------------TABLE FUNCTIONS------------------------------------------------------------------------------------------------------

        //generate IQueryable for manipulation by datatable 
        private IQueryable<CourseTableData> GetCourseTableQueryable()
        {

            //var roles = await RetreiveAllRolesAsync(); 
            var queryable = _context
            .Course.
            Select(c =>
            new CourseTableData()
            {
                CourseName = c.Name,
                NumberOfClasses = c.CourseClass.Count,
                CreatedDate = c.CreationTimestamp,
                UpdatedDate = c.UpdateTimestamp,
                DT_RowId = c.Id
            }
            );


            return queryable;
        }

        private IQueryable<CourseTableData> GlobalTableSearcher(IQueryable<CourseTableData> queryable, DTRequestHandler<CourseTableData> dtH)
        {

            //if search value is not empty 
            if (!string.IsNullOrEmpty(dtH.SearchValue))
            {
                queryable = queryable.Where(m => m.CourseName.Contains(dtH.SearchValue)

                                            );
            }

            return queryable; 
        }
        public async Task<DTResponse<CourseTableData>> GetCoursesDataTable(DTParameterModel dTParameterModel)
        {

            var dtH = new DTRequestHandler<CourseTableData>(dTParameterModel);



            var queryable = GetCourseTableQueryable(); 


            dtH.RecordsCounter(queryable);

            queryable = dtH.TableSorter(queryable);


            queryable = GlobalTableSearcher(queryable, dtH);


            queryable = dtH.TableFilterer(queryable);

            dtH.FilteredRecordsCounter(queryable);


            //skip 'start' records & Retrieve 'pagesize' records
            var data = await dtH.TablePager(queryable).ToListAsync();


            var dtResponse = new DTResponse<CourseTableData>()
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
