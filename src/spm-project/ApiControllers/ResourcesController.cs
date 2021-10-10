using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {

        public IUnitOfWork _unitOfWork;

        public ResourcesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        
    }
}
