using AutoMapper;
using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Common.Infrastructure.Extensions;
using Dictionary.Common.Models.Pages;
using Dictionary.Common.Models.QueriesModels;
using Dictionary.Common.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.Application.Features.Queries.GetEntries.GetMainPageEntries
{
    public class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery, PagedViewModel<GetEntryDetailViewModel>>
    {
        private readonly IEntryRepository entryRepository;

        public GetMainPageEntriesQueryHandler(IEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = entryRepository.AsQueryable();

            query = query.Include(s => s.EntryFavourites)
                         .Include(s => s.CreatedBy)
                         .Include(s => s.EntryVotes);

            var list = query.Select(s => new GetEntryDetailViewModel()
            {
                Id = s.Id,
                Subject = s.Subject,
                Content = s.Content,
                IsFavourited = request.UserId.HasValue && s.EntryFavourites.Any(x => x.CreatedById == request.UserId),
                FavouritedCount = s.EntryFavourites.Count,
                CreatedDate = s.CreateDate,
                CreatedByUserName = s.CreatedBy.UserName,
                VoteType = request.UserId.HasValue && s.EntryVotes.Any(x => x.CreatedById == request.UserId) ?
                           s.EntryVotes.FirstOrDefault(x => x.CreatedById == request.UserId).VoteType : VoteType.None
            });

            var entries = await list.GetPaged(request.Page, request.PageSize);

            return new PagedViewModel<GetEntryDetailViewModel>(entries.Results, entries.PageInfo);
        }
    }
}
