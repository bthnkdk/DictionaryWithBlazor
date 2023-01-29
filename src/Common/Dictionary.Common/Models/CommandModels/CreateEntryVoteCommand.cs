using Dictionary.Common.ViewModels;
using MediatR;

namespace Dictionary.Common.Models.CommandModels
{
    public class CreateEntryVoteCommand : IRequest<bool>
    {
        public Guid EntryId { get; set; }
        public Guid CreatedBy { get; set; }
        public VoteType VoteType { get; set; }
    }
}
