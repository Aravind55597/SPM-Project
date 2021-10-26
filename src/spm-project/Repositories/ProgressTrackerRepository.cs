using SPM_Project.Data;
using SPM_Project.EntityModels;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Repositories
{
    public class ProgressTrackerRepository: GenericRepository<ProgressTracker>, IProgressTrackerRepository
    {

        public ProgressTrackerRepository(ApplicationDbContext context) : base(context)
        {


        }


        public async Task<List<ProgressTracker>> GetCompletedProgressTracker(LMSUser user)
        {
            return _context.LMSUser.Where(u => u.Id == user.Id).First().ProgressTrackers.Where(p => p.Completed == true).ToList();
        }
    }
}
