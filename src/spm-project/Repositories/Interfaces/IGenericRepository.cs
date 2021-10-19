using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SPM_Project.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);


        Task RemoveByEntityAsync(T entity);

        Task RemoveRangeByEntityAsync(IEnumerable<T> entities);


        //Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);

        Task<List<T>> GetAllAsync(
    
             Expression<Func<T, bool>> filter=null,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy=null,
            string includeProperties="",
             int pageNumber=0,
             int pageSize=0
            );

        Task<T> GetByIdAsync(int id, string includeProperties = "");

        Task RemoveByIdAsync(int id);

        Task RemoveRangeByIdAsync(List<int> ids);




    }
}