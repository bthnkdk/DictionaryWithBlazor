namespace Dictionary.Api.Domain.Models;

public class EntryFavourite: BaseEntity
{
    public Guid EntryId { get; set; }
    public Guid CreatedById { get; set; }

    public virtual Entry Entry { get; set; }
    public virtual User CreatedUser { get; set; }
}
