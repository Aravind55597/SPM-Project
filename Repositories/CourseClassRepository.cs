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
    public class CourseClassRepository: GenericRepository<CourseClass>, ICourseClassRepository
    {

        public CourseClassRepository(ApplicationDbContext context) : base(context)
        {


        }



        //get classes for a trainer ->Accessed by a Trainer
        public async Task<DTResponse<CourseClassTableData>> GetCourseClassesForTrainerDataTable(DTParameterModel dTParameterModel, int LMSId)
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


            //retrieve course classes for the Trainer 
            var queryable = _context.CourseClass.
                Where(cc => cc.ClassTrainer.Id == LMSId).
                Select(cc=>new CourseClassTableData() { 
                
                    CourseName= cc.Course.Name, 
                    ClassName=cc.Name,
                    StartDate=cc.StartClass, 
                    EndDate = cc.EndClass,
                    NumOfChapters = cc.Chapters.Count(),
                    NumOfStudents = cc.ClassEnrollmentRecords.Where(ce=>ce.Approved).Count(),
                    DT_RowId=cc.Id
                });

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

            recordsTotal = queryable.Count();



            var data = await queryable.Skip(skip).Take(pageSize).ToListAsync();


            var dtResponse = new DTResponse<CourseClassTableData>()
            {
                Draw = draw,
                RecordsFiltered = recordsTotal,
                RecordsTotal = recordsTotal,
                Data = data,
            };

            return dtResponse;
        }

        public async Task<DTResponse<CourseClassTableData>> GetCourseClassesDataTable(DTParameterModel dTParameterModel)
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


            //retrieve course classes for the Trainer 
            var queryable = _context.CourseClass.
               Select(cc => new CourseClassTableData()
                {

                    CourseName = cc.Course.Name,
                    ClassName = cc.Name,
                    StartDate = cc.StartClass,
                    EndDate = cc.EndClass,
                    NumOfChapters = cc.Chapters.Count(),
                    NumOfStudents = cc.ClassEnrollmentRecords.Where(ce => ce.Approved).Count(),
                    DT_RowId = cc.Id
                });

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

            recordsTotal = queryable.Count();

            var data = await queryable.Skip(skip).Take(pageSize).ToListAsync();


            var dtResponse = new DTResponse<CourseClassTableData>()
            {
                Draw = draw,
                RecordsFiltered = recordsTotal,
                RecordsTotal = recordsTotal,
                Data = data,
            };

            return dtResponse;
        }

     
    }
}
