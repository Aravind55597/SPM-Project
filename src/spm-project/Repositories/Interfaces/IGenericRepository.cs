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

        Task<IEnumerable<T>> GetAllAsync(
    
             Expression<Func<T, bool>> filter,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            string includeProperties,
             int pageNumber,
             int pageSize
            );

        Task<T> GetByIdAsync(int id, string includeProperties = "");

        Task RemoveByIdAsync(int id);

        Task RemoveRangeByIdAsync(List<int> ids);




    }
}