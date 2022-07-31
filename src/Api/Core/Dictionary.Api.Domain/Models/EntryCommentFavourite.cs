namespace Dictionary.Api.Domain.Models;

public class EntryCommentFavourite : BaseEntity
{
    public Guid EntryCommentId { get; set; }
    public Guid CreatedById { get; set; }

    public virtual EntryComment EntryComment { get; set; }
    public virtual User CreatedUser { get; set; }
}
