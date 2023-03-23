using BeMo.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using BeMo.Models;
using BeMo.Repositories;

namespace BeMo.DataAccess;

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

    public Task<Challenge?> GetByPropertyAsync(Expression<Func<Challenge, bool>> propertyCondition)
    {
        return _context.Challenge.FirstOrDefaultAsync(propertyCondition);
    }

    public Task<bool> ExistsByPropertyAsync(Expression<Func<Challenge, bool>> propertyCondition)
    {
        return _context.Challenge.AnyAsync(propertyCondition);
    }

    public async Task InsertAsync(Challenge modelToInsert)
    {
        await _context.Challenge.AddAsync(modelToInsert);
        await _context.Instance.SaveChangesAsync();
    }

    public async Task UpdateAsync(Challenge modelToUpdate)
    {
        _context.Challenge.Update(modelToUpdate);
        await _context.Instance.SaveChangesAsync();
    }

    public async Task DeleteAsync(Challenge modelToDelete)
    {
        _context.Challenge.Remove(modelToDelete);
        await _context.Instance.SaveChangesAsync();
    }
}