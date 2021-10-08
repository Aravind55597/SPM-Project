using SPM_Project.Data;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.EntityModels;
using SPM_Project.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SPM_Project.Repositories
{
    public class LMSUserRepository : GenericRepository<LMSUser>, ILMSUserRepository
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _hcontext;
        private RoleManager<IdentityRole> _roleManager;

        public LMSUserRepository(ApplicationDbContext context , 
            UserManager<ApplicationUser> userManager , 
            IHttpContextAccessor hcontext , 
            RoleManager<IdentityRole> roleManager) : base(context)
        {
            _userManager = userManager;
            _hcontext = hcontext;
            _context = context;
            _roleManager = roleManager; 
        }


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


        //retreive all roles as a dictionary 

        public async Task<Dictionary<string,string>> RetreiveAllRolesAsync()
        {
            return await _roleManager.
                Roles.
                ToDictionaryAsync(
                r => r.Name,
                r => r.Id


                ); 
        }




        //--------------------------------------------TABLE FUNCTIONS------------------------------------------------------------------------------------------------------

        //generate IQueryable for manipulation by datatable 
        private IQueryable<LMSUsersTableData> GetLMSUsersTableQueryable()
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
                ) ;

            return queryable; 
        }



        //if courseId is 0 , dont have to check eligibility 
        //if classId is 0 , dont have to retreive based on class only
        //
        public async Task<DTResponse<LMSUsersTableData>> GetEngineersDataTable(DTParameterModel dTParameterModel)
        {


            var draw = dTParameterModel.Draw;
            var start = dTParameterModel.Start;
            var length = dTParameterModel.Start;
            var sortColumn = dTParameterModel.Columns[dTParameterModel.Order[0].Column].Data;
            var sortColumnDirection = dTParameterModel.Order[0].Dir;
            var searchValue = dTParameterModel.Search.Value;
            int pageSize = dTParameterModel.Length;
            
            
            //number of records to be skipped
            int skip = dTParameterModel.Start;
            int recordsTotal = 0;




            //Retrieve all userid + roleid pair that has either learnerRole or trainer role
            var queryable = GetLMSUsersTableQueryable().Where(q => q.Role == "Learner" || q.Role == "Trainer"); 


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


            recordsTotal = queryable.Count();

            //skip 'start' records & Retrieve 'pagesize' records
            var data = await queryable.Skip(skip).Take(pageSize).ToListAsync();

            var dtResponse = new DTResponse<LMSUsersTableData>()
            {
                Draw = draw,
                RecordsFiltered = recordsTotal,
                RecordsTotal = recordsTotal,
                Data = data,
            };

            return dtResponse; 


        }



        
     

    }
}