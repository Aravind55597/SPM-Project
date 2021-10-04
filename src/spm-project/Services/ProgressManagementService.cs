using SPM_Project.Repositories.Interfaces;
using SPM_Project.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Services
{
    public class ProgressManagementService:IProgressManagementService
    {


        public IUnitOfWork _unitOfWork;
        public ProgressManagementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
