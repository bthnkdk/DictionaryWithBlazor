using Dictionary.Api.Domain.Models;
using Dictionary.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dictionary.Infrastructure.Persistence.EntityConfigurations
{
    public class EntryCommentEntityConfiguration : BaseEntityConfiguration<EntryComment>
    {
        public override void Configure(EntityTypeBuilder<EntryComment> builder)
        {
            base.Configure(builder);

            builder.ToTable("entrycomment", DictionaryContext.DEFAULT_SCHEMA);

            builder.HasOne(p => p.CreatedBy)
                    .WithMany(p => p.EntryComments)
                    .HasForeignKey(p => p.CreatedById);


            builder.HasOne(p => p.Entry)
                    .WithMany(p => p.EntryComments)
                    .HasForeignKey(p => p.EntryId);
        }
    }
}
