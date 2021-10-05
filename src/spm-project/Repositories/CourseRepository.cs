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

namespace SPM_Project.Repositories
{
    public class CourseRepository :GenericRepository<Course>, ICourseRepository
    {

        public CourseRepository(ApplicationDbContext context) : base(context)
        {
            //TODO Retrieve course based on 

        }


        
        
        public async Task<DTResponse<CourseTableData>> GetCoursesDataTable(DTParameterModel dTParameterModel)
        {

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


            var queryable = _context
                .Course.
                Select(c =>
                new CourseTableData()
                {
                    CourseName=c.Name,
                    NumberOfClasses=c.CourseClass.Count, 
                    CreatedDate= c.CreationTimestamp,
                    UpdatedDate=c.UpdateTimestamp,
                    DT_RowId = c.Id
                }
                ); 
            

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
                                          
                                            );
            }

            recordsFiltered = queryable.Count();



            var data = await queryable.Skip(skip).Take(pageSize).ToListAsync();

            //repeated
            var dtResponse = new DTResponse<CourseTableData>()
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
