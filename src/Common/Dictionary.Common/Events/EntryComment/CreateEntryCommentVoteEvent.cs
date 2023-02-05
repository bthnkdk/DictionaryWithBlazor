using Dictionary.Common.ViewModels;

namespace Dictionary.Common.Events.EntryComment
{
    public class CreateEntryCommentVoteEvent
    {
        public Guid EntryCommentId { get; set; }
        public Guid CreatedBy { get; set; }
        public VoteType VoteType { get; set; }
    }
}
