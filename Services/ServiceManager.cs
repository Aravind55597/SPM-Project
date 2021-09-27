using SPM_Project.Repositories.Interfaces;
using SPM_Project.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Services
{
    public class ServiceManager:IServiceManager
    {

        public IUnitOfWork _unitOfWork; 


        public  ServiceManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CourseManagementService = new CourseManagementService(unitOfWork);
            ProgressManagementService = new ProgressManagementService(unitOfWork);


            UserManagementService = new UserManagementService(unitOfWork); 
        }

        public ICourseManagementService CourseManagementService { get; set; }


        public IProgressManagementService ProgressManagementService { get; set; }


    

        public IUserManagementService UserManagementService { get; set; }
    }
}
