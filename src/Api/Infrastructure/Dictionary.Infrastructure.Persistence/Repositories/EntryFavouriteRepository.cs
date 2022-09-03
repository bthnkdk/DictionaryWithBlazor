using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Api.Domain.Models;
using Dictionary.Infrastructure.Persistence.Context;

namespace Dictionary.Infrastructure.Persistence.Repositories
{
    public class EntryFavouriteRepository : GenericRepository<EntryFavourite>, IEntryFavouriteRepository
    {
        public EntryFavouriteRepository(DictionaryContext dbContext) : base(dbContext)
        {
        }
    }
}
