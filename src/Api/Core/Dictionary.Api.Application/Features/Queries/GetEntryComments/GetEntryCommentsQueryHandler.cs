using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Common.Infrastructure.Extensions;
using Dictionary.Common.Models.Pages;
using Dictionary.Common.Models.QueriesModels;
using Dictionary.Common.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.Application.Features.Queries.GetEntryComments
{
    public class GetEntryCommentsQueryHandler : IRequestHandler<GetEntryCommentsQuery, PagedViewModel<GetEntryCommentsViewModel>>
    {
        private readonly IEntryCommentRepository entryCommentRepository;

        public GetEntryCommentsQueryHandler(IEntryCommentRepository entryCommentRepository)
        {
            this.entryCommentRepository = entryCommentRepository;
        }

        public async Task<PagedViewModel<GetEntryCommentsViewModel>> Handle(GetEntryCommentsQuery request, CancellationToken cancellationToken)
        {
            var query = entryCommentRepository.AsQueryable();

            query = query.Include(s => s.EntryCommentFavourites)
                          .Include(s => s.EntryCommentVotes)
                          .Include(s => s.CreatedBy)
                          .Where(s => s.EntryId == request.EntryId);

            var list = query.Select(s => new GetEntryCommentsViewModel()
            {
                Id = s.Id,
                Content = s.Content,
                IsFavourited = request.UserId.HasValue && s.EntryCommentFavourites.Any(x => x.CreatedById == request.UserId),
                FavouritedCount = s.EntryCommentFavourites.Count,
                CreatedDate = s.CreateDate,
                CreatedByUserName = s.CreatedBy.UserName,
                VoteType = request.UserId.HasValue && s.EntryCommentVotes.Any(x => x.CreatedById == request.UserId) ?
                           s.EntryCommentVotes.FirstOrDefault(x => x.CreatedById == request.UserId).VoteType : VoteType.None
            });

            var entries = await list.GetPaged(request.Page, request.PageSize);

            return entries;
        }
    }
}
