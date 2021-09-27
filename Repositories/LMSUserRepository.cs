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

namespace SPM_Project.Repositories
{
    public class LMSUserRepository : GenericRepository<LMSUser>, ILMSUserRepository
    {
        public LMSUserRepository(ApplicationDbContext context) : base(context)
        {

        }


        //for now it will just return null
        public async Task<int?> RetreiveCurrentUserId() {

            return null;

        }

        //for now , it will just return null
        public async Task<bool?> CheckCurrentUserRole(string Role)
        {

            return null;


        }



        //get all engineers present -> Accessed by Trainer 
        public async Task<DTResponse<EngineersTableData>> GetEngineersDataTable(DTParameterModel dTParameterModel)
        {


            var draw = dTParameterModel.Draw;
            var start = dTParameterModel.Start;
            var length = dTParameterModel.Start;
            var sortColumn = dTParameterModel.Columns[dTParameterModel.Order[0].Column].Name;
            var sortColumnDirection = dTParameterModel.Order[0].Dir;
            var searchValue = dTParameterModel.Search.Value;
            int pageSize = dTParameterModel.Length;
            
            
            //number of records to be skipped
            int skip = dTParameterModel.Start;
            int recordsTotal = 0;


            //retreive learners and trainers role id 
            var learnerRole = _context.Roles.
                Where(r => r.Name == "Learner").
                Select(r => new { 
                Name=r.Name,
                Id=r.Id
                }).
                FirstOrDefault();

            //trainer role id 
            var trainerRole = _context.Roles.
                Where(r => r.Name == "Trainer").
                Select(r => new {
                    Name = r.Name,
                    Id = r.Id
                }).
                FirstOrDefault();

            //Retreive all userid + roleid pair that has either learnerRole or trainer role
            var queryable = _context.UserRoles.Where(ur => ur.RoleId == learnerRole.Id || ur.RoleId == trainerRole.Id)
                .Join(_context.Users,
                ur => ur.UserId,
                l => l.Id,
                (ur, l) =>
                new EngineersTableData
                {
                    Id=l.LMSUser.Id,
                    Name = l.Name,
                    Role = _context.Roles.Where(r => r.Id == ur.RoleId).Select(r => r.Name).FirstOrDefault()
                }
                );

      
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

            //skip 'start' records & retreive 'pagesize' records
            var data = await queryable.Skip(skip).Take(pageSize).ToListAsync();

            var dtResponse = new DTResponse<EngineersTableData>()
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