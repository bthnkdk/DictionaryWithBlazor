using Dictionary.Api.Domain.Models;
using Dictionary.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dictionary.Infrastructure.Persistence.EntityConfigurations
{
    public class EntryEntityConfiguration : BaseEntityConfiguration<Entry>
    {
        public override void Configure(EntityTypeBuilder<Entry> builder)
        {
            base.Configure(builder);

            builder.ToTable("entry", DictionaryContext.DEFAULT_SCHEMA);

            builder.HasOne(p => p.CreatedBy)
                    .WithMany(p => p.Entries)
                    .HasForeignKey(p => p.CreatedById);
        }
    }
}
