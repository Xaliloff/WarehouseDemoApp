using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.EntitiesConfiguration
{
    public class GoodsIssueNoteItemEntityConfig : IEntityTypeConfiguration<GoodsIssueNoteItem>
    {
        public void Configure(EntityTypeBuilder<GoodsIssueNoteItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedBy).HasMaxLength(255).IsRequired();

            builder.Property(x => x.LastModifiedBy).HasMaxLength(255).IsRequired(false);
        }
    }
}