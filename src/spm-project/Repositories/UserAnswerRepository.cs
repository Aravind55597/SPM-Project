using SPM_Project.Data;
using SPM_Project.EntityModels;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Repositories
{
    public class UserAnswerRepository: GenericRepository<UserAnswer>, IUserAnswerRepository
    {


        public UserAnswerRepository(ApplicationDbContext context) : base(context)
        {


        }




    }
}
