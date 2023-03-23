using System.Linq.Expressions;
using BeMo.Data;
using BeMo.Models;
using BeMo.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeMo.Repositories;

public class ActivityRepository : IRepository<Activity>
{
    private readonly IBeMoContext _context;
    private readonly DbSet<Activity> DbSet;

    public ActivityRepository(IBeMoContext context)
    {
        _context = context;
        DbSet = context.Instance.Set<Activity>();
    }

    public async Task<IEnumerable<Activity>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public Task<Activity?> GetByPropertyAsync(Expression<Func<Activity, bool>> predicate)
    {
        return DbSet.FirstOrDefaultAsync(predicate);
    }

    public Task<bool> ExistsByPropertyAsync(Expression<Func<Activity, bool>> predicate)
    {
        return DbSet.AnyAsync(predicate);
    }

    public async Task InsertAsync(Activity model)
    {
        await DbSet.AddAsync(model);
        await _context.Instance.SaveChangesAsync();
    }

    public async Task UpdateAsync(Activity model)
    {
        DbSet.Update(model);
        await _context.Instance.SaveChangesAsync();
    }

    public async Task DeleteAsync(Activity model)
    {
        DbSet.Remove(model);
        await _context.Instance.SaveChangesAsync();
    }
}
