using BeMo.Models;
using System.Linq.Expressions;
using BeMo.Data;
using Microsoft.EntityFrameworkCore;

namespace BeMo.Repositories;

public class ChallengeRepository : IRepository<Challenge>
{
    private readonly IBeMoContext _context;

    public ChallengeRepository(IBeMoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Challenge>> GetAllAsync()
    {
        return await _context.Challenge.ToListAsync();
    }

    public Task<Challenge?> GetByPropertyAsync(Expression<Func<Challenge, bool>> predicate)
    {
        return _context.Challenge.FirstOrDefaultAsync(predicate);
    }

    public Task<bool> ExistsByPropertyAsync(Expression<Func<Challenge, bool>> predicate)
    {
        return _context.Challenge.AnyAsync(predicate);
    }

    public async Task InsertAsync(Challenge model)
    {
        await _context.Challenge.AddAsync(model);
        await _context.Instance.SaveChangesAsync();
    }

    public async Task UpdateAsync(Challenge model)
    {
        _context.Challenge.Update(model);
        await _context.Instance.SaveChangesAsync();
    }

    public async Task DeleteAsync(Challenge model)
    {
        _context.Challenge.Remove(model);
        await _context.Instance.SaveChangesAsync();
    }
}