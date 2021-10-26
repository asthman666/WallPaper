using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WallPaper.Core.Entities;

namespace WallPaper.Core.Interfaces
{
    public interface IRepository
    {
        bool Any<T>() where T : BaseEntity;
        bool Any<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity;

        Task<T> GetByIdAsync<T>(int id) where T : BaseEntity;
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity;
        Task<List<T>> ListAsync<T>() where T : BaseEntity;
        Task<T> AddAsync<T>(T entity) where T : BaseEntity;
        Task UpdateAsync<T>(T entity) where T : BaseEntity;
        Task DeleteAsync<T>(T entity) where T : BaseEntity;
    }
}
