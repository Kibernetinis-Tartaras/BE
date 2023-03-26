using BeMo.Models;
using System.Linq.Expressions;

namespace BeMo.Repositories.Interfaces
{
    public interface IUserChallengeRepository : IRepository<User_Challenge>
    {
        public Task<IEnumerable<User_Challenge>> GetAllByPropertyAsync(Expression<Func<User_Challenge, bool>> predicate);
    }
}
