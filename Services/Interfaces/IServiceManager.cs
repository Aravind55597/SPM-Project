using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Services.Interfaces
{
    public interface IServiceManager
    {
        public ICourseManagementService CourseManagementService { get; set; }


        public IProgressManagementService ProgressManagementService { get; set; }


        public IResourceManagementService ResourceManagementService { get; set; }

        public IUserManagementService UserManagementService { get; set; }
    }
}
