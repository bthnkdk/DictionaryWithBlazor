using Dictionary.Api.Domain.Models;
using Dictionary.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dictionary.Infrastructure.Persistence.EntityConfigurations
{
    public class EntryCommentFavouriteEntityConfiguration : BaseEntityConfiguration<EntryCommentFavourite>
    {
        public override void Configure(EntityTypeBuilder<EntryCommentFavourite> builder)
        {
            base.Configure(builder);

            builder.ToTable("entrycommentfavourite", DictionaryContext.DEFAULT_SCHEMA);

            builder.HasOne(p => p.EntryComment)
                    .WithMany(p => p.EntryCommentFavourites)
                    .HasForeignKey(p => p.EntryCommentId);


            builder.HasOne(p => p.CreatedUser)
                    .WithMany(p => p.EntryCommentFavourites)
                    .HasForeignKey(p => p.CreatedById)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
