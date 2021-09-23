using SPM_Project.Data;
using SPM_Project.EntityModels;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Repositories
{
    public class ApplicationUserRepository : GenericRepository<ApplicationUser> ,IApplicationUserRepository
    {


        //constructor injection 
        //can use the _context attribut of the parent class 
        public ApplicationUserRepository(ApplicationDbContext context):base(context)
        {
       

        }


    }
}
