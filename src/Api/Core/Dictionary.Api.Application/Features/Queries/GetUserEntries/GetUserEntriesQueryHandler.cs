using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Common.Infrastructure.Extensions;
using Dictionary.Common.Models.Pages;
using Dictionary.Common.Models.QueriesModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.Application.Features.Queries.GetUserEntries
{
    public class GetUserEntriesQueryHandler : IRequestHandler<GetUserEntriesQuery, PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        private readonly IEntryRepository entryRepository;

        public GetUserEntriesQueryHandler(IEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }

        public async Task<PagedViewModel<GetUserEntriesDetailViewModel>> Handle(GetUserEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = entryRepository.AsQueryable();

            if (request.UserId != null && request.UserId.HasValue && request.UserId != Guid.Empty)
                query = query.Where(s => s.CreatedById == request.UserId);
            else if (!string.IsNullOrEmpty(request.UserName))
                query = query.Where(s => s.CreatedBy.UserName == request.UserName);
            else 
                return null;

            query = query.Include(s => s.EntryFavourites)
                         .Include(s => s.CreatedBy);

            var list = query.Select(s => new GetUserEntriesDetailViewModel()
            {
                Id = s.Id,
                Subject = s.Subject,
                Content = s.Content,
                IsFavourited = false,
                FavouritedCount = s.EntryFavourites.Count,
                CreatedDate = s.CreateDate,
                CreatedByUserName = s.CreatedBy.UserName
            });

            var entries = await list.GetPaged(request.Page, request.PageSize);

            return entries;
        }
    }
}
