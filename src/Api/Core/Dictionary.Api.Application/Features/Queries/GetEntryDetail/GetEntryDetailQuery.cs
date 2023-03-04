using Dictionary.Common.Models.QueriesModels;
using MediatR;

namespace Dictionary.Api.Application.Features.Queries.GetEntryDetail
{
    public class GetEntryDetailQuery : IRequest<GetEntryDetailViewModel>
    {
        public Guid EntryId { get; set; }
        public Guid? UserId { get; set; }

        public GetEntryDetailQuery(Guid? userId, Guid entryId)
        {
            UserId = userId;
            EntryId = entryId;
        }
    }
}
