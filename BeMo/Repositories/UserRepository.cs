using System.Linq.Expressions;
using BeMo.Models;
using BeMo.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeMo.Repositories;

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

    public Task<User?> GetByPropertyAsync(Expression<Func<User, bool>> predicate)
    {
        return _context.User.FirstOrDefaultAsync(predicate);
    }

    public Task<bool> ExistsByPropertyAsync(Expression<Func<User, bool>> predicate)
    {
        return _context.User.AnyAsync(predicate);
    }

    public async Task InsertAsync(User model)
    {
        await _context.User.AddAsync(model);
        await _context.Instance.SaveChangesAsync();
    }

    public async Task UpdateAsync(User model)
    {
        _context.User.Update(model);
        await _context.Instance.SaveChangesAsync();
    }

    public async Task DeleteAsync(User model)
    {
        _context.User.Remove(model);
        await _context.Instance.SaveChangesAsync();
    }
}
