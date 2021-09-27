using Microsoft.AspNetCore.Mvc;
using SPM_Project.Data;
using SPM_Project.DataTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using SPM_Project.DataTableModels.DataTableResponse;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPM_Project.PracticeDatatableModelAndController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private ApplicationDbContext _dbContext; 

        public CustomerController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // POST api/<CustomerController>
        [HttpPost, Route("Customers", Name = "RetrieveCustomers")]
        public IActionResult Customers([FromBody] DTParameterModel customerTable)
        {

            try
            {
                var draw = customerTable.Draw;
                var start = customerTable.Start;
                var length = customerTable.Start;

                var sortColumn = customerTable.Columns[customerTable.Order[0].Column].Name; 


                var sortColumnDirection = customerTable.Order[0].Dir;
                var searchValue = customerTable.Search.Value;
                int pageSize = customerTable.Length;
                //number of records to be skipped
                int skip = customerTable.Start;
                int recordsTotal = 0;






                var customerQueryable = _dbContext.Customer.AsQueryable();


                //if sortcolumn and sort colum direction are not empty 
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    customerQueryable = customerQueryable.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                //if search value is not empty 
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerQueryable = customerQueryable.Where(m => m.FirstName.Contains(searchValue)
                                                || m.LastName.Contains(searchValue)
                                                || m.Contact.Contains(searchValue)
                                                || m.Email.Contains(searchValue));
                }

                recordsTotal = customerQueryable.Count();

                //skip 'start' records & retreive 'pagesize' records
                var data =customerQueryable.Skip(skip).Take(pageSize).Select(c=>new CustomerDT() { 
                
                Id=c.Id,
                FirstName=c.FirstName,
                LastName=c.LastName,
                Contact=c.Contact,
                Email=c.Email,
                DateOfBirth=c.DateOfBirth,
                DT_RowId=c.Id,
                DT_RowClass="test",
                DT_RowData=new Dictionary<dynamic, dynamic>()
                {
                    {"Id",c.Id },
                    {"FirstName",c.FirstName },
                    {"LastName",c.LastName },
                }


                }).ToList();

                var dtResponse = new DTResponse<CustomerDT>()
                {
                    Draw = draw,
                    RecordsFiltered = recordsTotal,
                    RecordsTotal = recordsTotal,
                    Data = data,
                };

                var response = Newtonsoft.Json.JsonConvert.SerializeObject(dtResponse);
                return Ok(response); 
            }

            catch (Exception ex)
            {
                throw; 
            }


        }




    }
}
