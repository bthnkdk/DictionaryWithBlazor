using Dictionary.Common.ViewModels;
using MediatR;

namespace Dictionary.Common.Models.CommandModels
{
    public class CreateEntryCommentVoteCommand : IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }
        public Guid CreatedBy { get; set; }
        public VoteType VoteType { get; set; }

        public CreateEntryCommentVoteCommand()
        {

        }
        public CreateEntryCommentVoteCommand(Guid entryCommentId, Guid createdBy, VoteType voteType)
        {
            EntryCommentId = entryCommentId;
            CreatedBy = createdBy;
            VoteType = voteType;
        }
    }
}
