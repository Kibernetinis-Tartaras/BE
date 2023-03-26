using BeMo.Models;
using System.Linq.Expressions;

namespace BeMo.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync();

    public Task<T?> GetByPropertyAsync(Expression<Func<T, bool>> predicate);

    public Task<bool> ExistsByPropertyAsync(Expression<Func<T, bool>> predicate);

    public Task InsertAsync(T model);

    public Task UpdateAsync(T model);

    public Task DeleteAsync(T model);
}
