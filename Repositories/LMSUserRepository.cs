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
using SPM_Project.DataTableModels.DataTableDataInterface;
using Microsoft.EntityFrameworkCore;

namespace SPM_Project.Repositories
{
    public class LMSUserRepository : GenericRepository<LMSUser>, ILMSUserRepository
    {
        public LMSUserRepository(ApplicationDbContext context) : base(context)
        {

        }

        //get dictioanry of role name & role id 





        public async Task<DTResponse> GetEngineersDataTable(DTParameterModel dTParameterModel)
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



            //get queryable ----------------------------------------------------------------------------------------------------------------------------------

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
            var engineersQueryable = _context.UserRoles.Where(ur => ur.RoleId == learnerRole.Id || ur.RoleId == trainerRole.Id)
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

            //get queryable ----------------------------------------------------------------------------------------------------------------------------------




            //if sortcolumn and sort colum direction are not empty 
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                engineersQueryable = engineersQueryable.OrderBy(sortColumn + " " + sortColumnDirection);
            }

            //if search value is not empty 
            if (!string.IsNullOrEmpty(searchValue))
            {
                engineersQueryable = engineersQueryable.Where(m => m.Name.Contains(searchValue)
                                            || m.Role.Contains(searchValue));
            }

            //--------------------------------------------------------------------------------------------------------------------------------


            recordsTotal = engineersQueryable.Count();

            //skip 'start' records & retreive 'pagesize' records
            var data = await engineersQueryable.Skip(skip).Take(pageSize).Select(e => (IDTData)new EngineersTableData()
            {

                Id = e.Id,
                Name = e.Name,
                Role = e.Role,
                DT_RowId = e.Id


            }).ToListAsync();

            var dtResponse = new DTResponse()
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