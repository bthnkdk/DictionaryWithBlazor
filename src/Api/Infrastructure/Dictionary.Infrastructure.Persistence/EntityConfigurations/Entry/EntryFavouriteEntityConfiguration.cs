using Dictionary.Api.Domain.Models;
using Dictionary.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dictionary.Infrastructure.Persistence.EntityConfigurations
{
    public class EntryFavouriteEntityConfiguration : BaseEntityConfiguration<EntryFavourite>
    {
        public override void Configure(EntityTypeBuilder<EntryFavourite> builder)
        {
            base.Configure(builder);

            builder.ToTable("entryfavourite", DictionaryContext.DEFAULT_SCHEMA);

            builder.HasOne(p => p.Entry)
                    .WithMany(p => p.EntryFavourites)
                    .HasForeignKey(p => p.EntryId);

            builder.HasOne(p => p.CreatedUser)
                   .WithMany(p => p.EntryFavourites)
                   .HasForeignKey(p => p.CreatedById);
        }
    }
}
