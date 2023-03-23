using System.Linq.Expressions;

namespace BeMo.Repositories;

public interface IRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetAllAsync();

    public Task<T?> GetByPropertyAsync(Expression<Func<T, bool>> propertyCondition);

    public Task<bool> ExistsByPropertyAsync(Expression<Func<T, bool>> propertyCondition);

    public Task InsertAsync(T modelToInsert);

    public Task UpdateAsync(T modelToUpdate);

    public Task DeleteAsync(T modelToDelete);
}
