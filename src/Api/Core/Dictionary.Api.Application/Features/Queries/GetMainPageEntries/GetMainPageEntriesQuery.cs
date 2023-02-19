using Dictionary.Common.Models.Pages;
using Dictionary.Common.Models.QueriesModels;
using MediatR;

namespace Dictionary.Api.Application.Features.Queries.GetEntries.GetMainPageEntries
{
    public class GetMainPageEntriesQuery : BasePagedQuery, IRequest<PagedViewModel<GetEntryDetailViewModel>>
    {
        public GetMainPageEntriesQuery(Guid? userId, int page, int pageSize) : base(page, pageSize)
        {
            UserId = userId;
        }

        public Guid? UserId { get; set; }
    }
}
