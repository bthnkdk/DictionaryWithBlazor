namespace Dictionary.Api.Domain.Models;

public class EntryCommentFavourite
{
    public Guid EntryCommentId { get; set; }
    public Guid CreatedByCommentId { get; set; }

    public virtual EntryComment EntryComment { get; set; }
    public virtual User CreatedUser { get; set; }
}
