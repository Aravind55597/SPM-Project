using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SPM_Project.Repositories.Interfaces
{
    internal interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task RemoveAsync(T entity);

        Task RemoveRange(IEnumerable<T> entities);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);

        Task<IEnumerable<T>> GetAllAsync(

             Expression<Func<T, bool>> filter,
             Expression<Func<T, bool>> orderBy,
            string includeProperties,
            bool asc

            );

        Task<T> GetByIdAsync(int id);

        Task RemoveByIdAsync(int id);

        Task RemoveRangeAsync(List<int> ids);

    }
}