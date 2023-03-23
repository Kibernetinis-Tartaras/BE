using System.Linq.Expressions;
using BeMo.Models;
using BeMo.Repositories;

namespace BeMo.Models;

public class ActivityRepository : IRepository<Activity>
{
    private readonly IBeMoContext _context;

    public ActivityRepository(IBeMoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Activity>> GetAllAsync()
    {
        return await _context.Activity.ToListAsync();
    }

    public Task<Activity?> GetByPropertyAsync(Expression<Func<Activity, bool>> propertyCondition)
    {
        return _context.Activity.FirstOrDefaultAsync(propertyCondition);
    }

    public Task<bool> ExistsByPropertyAsync(Expression<Func<Activity, bool>> propertyCondition)
    {
        return _context.Activity.AnyAsync(propertyCondition);
    }

    public async Task InsertAsync(Activity modelToInsert)
    {
        await _context.Activity.AddAsync(modelToInsert);
        await _context.Instance.SaveChangesAsync();
    }

    public async Task UpdateAsync(Activity modelToUpdate)
    {
        _context.Activity.Update(modelToUpdate);
        await _context.Instance.SaveChangesAsync();
    }

    public async Task DeleteAsync(Activity modelToDelete)
    {
        _context.Activity.Remove(modelToDelete);
        await _context.Instance.SaveChangesAsync();
    }
}
