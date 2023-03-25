using BeMo.Data;
using BeMo.Models;
using BeMo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BeMo.Repositories
{
    public class User_ChallengeRepository: IUserChallengeRepository
    {

        private readonly IBeMoContext _context;

        public User_ChallengeRepository(IBeMoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User_Challenge>> GetAllAsync()
        {
            return await _context.User_Challenge.ToListAsync();
        }

        public Task<User_Challenge?> GetByPropertyAsync(Expression<Func<User_Challenge, bool>> predicate)
        {
            return _context.User_Challenge.FirstOrDefaultAsync(predicate);
        }

        public Task<bool> ExistsByPropertyAsync(Expression<Func<User_Challenge, bool>> predicate)
        {
            return _context.User_Challenge.AnyAsync(predicate);
        }

        public async Task InsertAsync(User_Challenge model)
        {
            await _context.User_Challenge.AddAsync(model);
            await _context.Instance.SaveChangesAsync();
        }

        public async Task UpdateAsync(User_Challenge model)
        {
            _context.User_Challenge.Update(model);
            await _context.Instance.SaveChangesAsync();
        }

        public async Task DeleteAsync(User_Challenge model)
        {
            _context.User_Challenge.Remove(model);
            await _context.Instance.SaveChangesAsync();
        }

        public async Task<IEnumerable<User_Challenge>> GetAllByPropertyAsync(Expression<Func<User_Challenge, bool>> predicate)
        {
            return from user_Challenge in await _context.User_Challenge
                   .Where(predicate).ToListAsync() select user_Challenge;
        }
    }
}
