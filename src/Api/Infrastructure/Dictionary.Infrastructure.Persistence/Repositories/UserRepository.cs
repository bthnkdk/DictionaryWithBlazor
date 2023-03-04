using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Api.Domain.Models;
using Dictionary.Infrastructure.Persistence.Context;

namespace Dictionary.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DictionaryContext dbContext) : base(dbContext)
        {
        }
    }
}
