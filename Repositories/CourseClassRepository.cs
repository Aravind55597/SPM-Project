using SPM_Project.Data;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.EntityModels;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Repositories
{
    public class CourseClassRepository: GenericRepository<CourseClass>, ICourseClassRepository
    {

        public CourseClassRepository(ApplicationDbContext context) : base(context)
        {


        }







    }
}
