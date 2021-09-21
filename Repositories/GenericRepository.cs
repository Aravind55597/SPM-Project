﻿using Microsoft.EntityFrameworkCore;
using SPM_Project.Data;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SPM_Project.Repositories
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
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


        //retrieve all values (stupid to use on large data sets )
        //NEED To implement pagination later 
        public async virtual Task<IEnumerable<T>> GetAllAsync(
            //lambda to filter
            Expression<Func<T, bool>> filter = null,
            //lambda to order
            //q => q.OrderBy(s => s.LastName)
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            //properties to filter by 
            string includeProperties = "",
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

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

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


        //retreive data by Id 
        public virtual async Task<T> GetByIdAsync(int id)
        {
            var data = await _context.Set<T>().FindAsync(id);
            return data;
        }

        //UPDATE------------------------------------------------------------------------------------------------


























        //DELETE------------------------------------------------------------------------------------------------

        //remove an enitity  
        public virtual async Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        //remove range of entities from the parent entity 
        public virtual async Task RemoveRange(IEnumerable<T> entities)
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
        public virtual async Task RemoveRangeAsync(List<int> ids)
        {
            for (int i = 0; i < ids.Count; i++)
            {
                var data = await _context.Set<T>().FindAsync(ids[i]);
                _context.Set<T>().Remove(data);
            }
            
        }

    }












}
