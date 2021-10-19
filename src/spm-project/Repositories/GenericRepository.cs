using Microsoft.EntityFrameworkCore;
using SPM_Project.Data;
using SPM_Project.DataTableModels.DataTableRequest;
using SPM_Project.EntityModels;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace SPM_Project.Repositories
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class, IEntityWithId
    {
        public ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
 
        }

        //CREATE------------------------------------------------------------------------------------------------


        //add entities to database
        public virtual async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        //add range of entities to database 
        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        //READ------------------------------------------------------------------------------------------------

        //refernce : https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
        public async virtual Task<List<T>> GetAllAsync(
            //properties to filter by 
            string includeProperties = "",
            //student => student.LastName == "Smith" 
            //lambda to filter
            Expression<Func<T, bool>> filter = null,
            //lambda to order
            //q => q.OrderBy(s => s.LastName)
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            //page number
            int pageNumber = 0,
            //size of the page
            int pageSize =0
            )
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = IncludeFunc(query,  includeProperties); 

            if (orderBy != null)
            {
                orderBy(query);
            }
      

            if (pageNumber!=0 && pageSize != 0)
            {
                query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }

            var data = await query.ToListAsync();

            return data; 

        }


        //Retrieve data by Id 
        public virtual async Task<T> GetByIdAsync(int id, string includeProperties = "")
        {
            IQueryable<T> query = _context.Set<T>();

            if (includeProperties!="")
            {
                query = IncludeFunc(query, includeProperties);
            }

            return await query.FirstOrDefaultAsync(q=>q.Id==id);
        }
        //GetByIdAsync(1,"PreRequisites")

        private IQueryable<T> IncludeFunc(IQueryable<T> query , string includeProperties )
        {
            foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {

                if (typeof(T).GetProperty(includeProperty)!=null)
                {
                    query = query.Include(includeProperty);
                }
               
            }

            return query; 
        }


        //UPDATE------------------------------------------------------------------------------------------------


        //DELETE------------------------------------------------------------------------------------------------

        //remove an enitity  
        public virtual async Task RemoveByEntityAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        //remove range of entities from the parent entity 
        public virtual async Task RemoveRangeByEntityAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }



        //remove data by Id
        public virtual async Task RemoveByIdAsync(int id )
        {
          var data = await _context.Set<T>().FindAsync(id);
          _context.Set<T>().Remove(data); 

        }

        //remove range of entities by Id
        public virtual async Task RemoveRangeByIdAsync(List<int> ids)
        {
            for (int i = 0; i < ids.Count; i++)
            {
                var data = await _context.Set<T>().FindAsync(ids[i]);
                _context.Set<T>().Remove(data);
            }
            
        }





    }










}
