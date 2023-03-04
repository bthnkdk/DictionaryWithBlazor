using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Common.Models.QueriesModels;
using Dictionary.Common.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.Application.Features.Queries.GetEntryDetail
{
    public class GetEntryDetailQueryHandler : IRequestHandler<GetEntryDetailQuery, GetEntryDetailViewModel>
    {
        private readonly IEntryRepository entryRepository;

        public GetEntryDetailQueryHandler(IEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }

        public async Task<GetEntryDetailViewModel> Handle(GetEntryDetailQuery request, CancellationToken cancellationToken)
        {
            var query = entryRepository.AsQueryable();

            query = query.Include(s => s.EntryFavourites)
                         .Include(s => s.CreatedBy)
                         .Include(s => s.EntryVotes)
                         .Where(s => s.Id == request.EntryId);

            var result = query.Select(s => new GetEntryDetailViewModel()
            {
                Id = s.Id,
                Subject = s.Subject,
                Content = s.Content,
                IsFavourited = request.UserId.HasValue && s.EntryFavourites.Any(x => x.CreatedById == request.UserId),
                FavouritedCount = s.EntryFavourites.Count,
                CreatedDate = s.CreateDate,
                CreatedByUserName = s.CreatedBy.UserName,
                VoteType = request.UserId.HasValue && s.EntryVotes.Any(x => x.CreatedById == request.UserId) ?
                           s.EntryComments.FirstOrDefault(x => x.CreatedById == request.UserId).VoteType : VoteType.None
            });

            return await result.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
