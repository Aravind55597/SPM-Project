using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Services
{
    public class CourseManagementService
    {

        public IUnitOfWork _UnitOfWork;

        public CourseManagementService(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork; 
        }












    }
}
