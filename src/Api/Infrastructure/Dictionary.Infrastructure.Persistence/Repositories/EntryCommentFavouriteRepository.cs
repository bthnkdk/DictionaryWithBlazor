using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Api.Domain.Models;
using Dictionary.Infrastructure.Persistence.Context;

namespace Dictionary.Infrastructure.Persistence.Repositories
{
    public class EntryCommentFavouriteRepository : GenericRepository<EntryCommentFavourite>, IEntryCommentFavouriteRepository
    {
        public EntryCommentFavouriteRepository(DictionaryContext dbContext) : base(dbContext)
        {
        }
    }
}
