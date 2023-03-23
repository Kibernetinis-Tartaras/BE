using System.Linq.Expressions;
using BeMo.Data;
using BeMo.Models;
using BeMo.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeMo.Repositories;

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

    public Task<Activity?> GetByPropertyAsync(Expression<Func<Activity, bool>> predicate)
    {
        return _context.Activity.FirstOrDefaultAsync(predicate);
    }

    public Task<bool> ExistsByPropertyAsync(Expression<Func<Activity, bool>> predicate)
    {
        return _context.Activity.AnyAsync(predicate);
    }

    public async Task InsertAsync(Activity model)
    {
        await _context.Activity.AddAsync(model);
        await _context.Instance.SaveChangesAsync();
    }

    public async Task UpdateAsync(Activity model)
    {
        _context.Activity.Update(model);
        await _context.Instance.SaveChangesAsync();
    }

    public async Task DeleteAsync(Activity model)
    {
        _context.Activity.Remove(model);
        await _context.Instance.SaveChangesAsync();
    }
}
