using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SPM_Project.Data;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels.DataTableRequest;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.EntityModels;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SPM_Project.Repositories
{
    public class LMSUserRepository : GenericRepository<LMSUser>, ILMSUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _hcontext;
        private RoleManager<IdentityRole> _roleManager;


        public LMSUserRepository(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor hcontext,
            RoleManager<IdentityRole> roleManager) : base(context)
        {
            _userManager = userManager;
            _hcontext = hcontext;
            _context = context;
            _roleManager = roleManager;
        }










        //retreive all roles as a dictionary



        public async Task<Dictionary<string, string>> RetreiveAllRolesAsync()
        {
            return await _roleManager.
                Roles.
                ToDictionaryAsync(
                r => r.Name,
                r => r.Id

                );
        }


        //--------------------------------------------CURRENT-USER------------------------------------------------------------------------------------------------------

        //retrieve lMSUser id of current user
        public async Task<int> RetrieveCurrentUserIdAsync()
        {
            //retreive app user id
            var appUserId = _hcontext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var lmsUserId = await _context.Users.Where(u => u.Id == appUserId).Select(u => u.LMSUser.Id).FirstOrDefaultAsync();

            return lmsUserId;
        }

        //retreive role of current user
        public async Task<List<string>> RetreiveUserRolesAsync(int LMSUserId)
        {
            var appUserId = await _context.Users
                .Where(u => u.LMSUser.Id == LMSUserId)
                .Select(u => u.Id).FirstOrDefaultAsync();

            var user = await _userManager.FindByIdAsync(appUserId);

            var roles = await _userManager.GetRolesAsync(user);

            return (List<string>)roles;
        }





        //--------------------------------------------TABLE FUNCTIONS------------------------------------------------------------------------------------------------------

        //generate IQueryable for manipulation by datatable

        private IQueryable<LMSUsersTableData> GetLMSUsersTableQueryable(bool isTrainer, bool isLearner, bool isEligible , int? classId)
        {
            //var roles = await RetreiveAllRolesAsync();
            var queryable = _context.UserRoles
                .Join(_context.Users,
                ur => ur.UserId,
                l => l.Id,
                (ur, l) =>
                new LMSUsersTableData
                {
                    Id = l.LMSUser.Id,
                    Name = l.Name,
                    Role = _context.Roles.Where(r => r.Id == ur.RoleId).Select(r => r.Name).FirstOrDefault(),
                    Department = l.Department.ToString(),
                    DOB = l.DOB
                }
                );


            if (classId==null)
            {
                //returns trainer and learner
                return queryable; 
            }
            else
            {

                //class id not null, so return class only trainer and learner?

                if(!isEligible)
                {
                   //return all class trainer and learner
                    var userIdsInClass = _context.LMSUser.
                    Where(l => l.Enrollments.Any(e => e.Approved == true && e.CourseClass.Id == classId) || l.Id == _context.CourseClass.Where(cc => cc.Id == classId).
                    Select(cc => cc.ClassTrainer.Id).FirstOrDefault()).Select(l => new { Id = l.Id });

                    queryable =
                    queryable.Join(userIdsInClass,
                    l => l.Id,
                    u => u.Id,
                    (l, u) =>
                    new LMSUsersTableData
                    {
                        Id = l.Id,
                        Name = l.Name,
                        Role = l.Role,
                        Department = l.Department,
                        DOB = l.DOB
                    }
                    );


                    return queryable; 

                }


                if (isLearner)
                {
                    queryable = queryable.Where(q => q.Role == "Learner");

                    //check the prerequisite of the class 
                    var preReq = _context.CourseClass.Where(cc => cc.Id == classId).Select(cc => cc.Course).SelectMany(c => c.PreRequisites).Select(p => p.Id);
                    //return Queryable
                    
                    //check if all  the prereq course ids are present in 
                    queryable.
                        Where(q => preReq.All(    _context.LMSUser.Where(l => l.Id == q.Id).SelectMany(l => l.Enrollments).Where(e=>e.CompletionStatus).Select(e => e.CourseClass.Course.Id).Contains    )    
                    ); 
                }

                if (isTrainer)
                {
                    throw new NotImplementedException("We have not implemented the functionality to check for eligible trainers"); 
                }


            }


            ////if user chooses Trainer or learner
            //if (isTrainer)
            //{
            //    queryable = queryable.Where(q => q.Role == "Trainer");
            //}
            //else if (isLearner)
            //{
            //    queryable = queryable.Where(q => q.Role == "Learner");
            //}

            //if user chooses a specific class


            return queryable;
        }

       
        
        
        
        
        
        
        //if courseId is 0 , dont have to check eligibility
        //if classId is 0 , dont have to retreive based on class only
        // bool isTrainer, bool isLearner, bool isEligible, int? classId
        public async Task<DTResponse<LMSUsersTableData>> GetEngineersDataTable(DTParameterModel dTParameterModel, bool isTrainer, bool isLearner, bool isEligible , int? classId)
        {
            var draw = dTParameterModel.Draw;
            var start = dTParameterModel.Start;
            var length = dTParameterModel.Start;
            var sortColumn = dTParameterModel.Columns[dTParameterModel.Order[0].Column].Data;
            var sortColumnDirection = dTParameterModel.Order[0].Dir;
            var searchValue = dTParameterModel.Search.Value;
            int pageSize = dTParameterModel.Length;
            List<DTFilter> filterList = dTParameterModel.Filter; 

            //number of records to be skipped
            int skip = dTParameterModel.Start;
            int recordsTotal = 0;
            int recordsFiltered = 0;

            //Retrieve all userid + roleid pair that has either learnerRole or trainer role
            var queryable = GetLMSUsersTableQueryable(isTrainer, isLearner, isEligible , classId).Where(q => q.Role == "Learner" || q.Role == "Trainer");

            recordsTotal = queryable.Count();

       
            //if sortcolumn and sort colum direction are not empty
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                queryable = queryable.OrderBy(sortColumn + " " + sortColumnDirection);
            }

            //if search value is not empty
            if (!string.IsNullOrEmpty(searchValue))
            {
                queryable = queryable.Where(m => m.Name.Contains(searchValue)
                                            || m.Role.Contains(searchValue));
            }


            //filter 
            if (filterList!=null)
            {
                foreach (DTFilter filter in filterList)
                {
                    if (filter.Column!=null && filter.Value != null)
                    {
                        queryable.Where("@0 = @1", filter.Column, filter.Value); 
                    }

                }
            }


            recordsFiltered = queryable.Count();

            //skip 'start' records & Retrieve 'pagesize' records
            var data = await queryable.Skip(skip).Take(pageSize).ToListAsync();

            var dtResponse = new DTResponse<LMSUsersTableData>()
            {
                Draw = draw,
                RecordsFiltered = recordsTotal,
                RecordsTotal = recordsFiltered,
                Data = data,
            };

            return dtResponse;
        }
    }
}