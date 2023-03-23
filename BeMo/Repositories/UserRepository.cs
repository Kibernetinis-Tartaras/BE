using System.Linq.Expressions;
using BeMo.Models;
using BeMo.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeMo.DataAccess;

public class UserRepository : IRepository<User>
{
    private readonly IBeMoContext _context;

    public UserRepository(IBeMoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.User.ToListAsync();
    }

    public Task<User?> GetByPropertyAsync(Expression<Func<User, bool>> propertyCondition)
    {
        return _context.User.FirstOrDefaultAsync(propertyCondition);
    }

    public Task<bool> ExistsByPropertyAsync(Expression<Func<User, bool>> propertyCondition)
    {
        return _context.User.AnyAsync(propertyCondition);
    }

    public async Task InsertAsync(User modelToInsert)
    {
        await _context.User.AddAsync(modelToInsert);
        await _context.Instance.SaveChangesAsync();
    }

    public async Task UpdateAsync(User modelToUpdate)
    {
        _context.User.Update(modelToUpdate);
        await _context.Instance.SaveChangesAsync();
    }

    public async Task DeleteAsync(User modelToDelete)
    {
        _context.User.Remove(modelToDelete);
        await _context.Instance.SaveChangesAsync();
    }
}
