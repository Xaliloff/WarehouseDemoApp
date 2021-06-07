using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.EntitiesConfiguration
{
    public class GoodsReceiptNoteEntityConfig : IEntityTypeConfiguration<GoodsReceiptNote>
    {
        public void Configure(EntityTypeBuilder<GoodsReceiptNote> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AcceptedBy).HasMaxLength(255).IsRequired();
            builder.Property(x => x.VendorName).HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.CreatedBy).HasMaxLength(255).IsRequired();
            builder.Property(x => x.LastModifiedBy).HasMaxLength(255).IsRequired(false);
        }
    }
}