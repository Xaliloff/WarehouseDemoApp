using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.EntitiesConfiguration
{
    public class WarehouseTransactionsEntityConfig : IEntityTypeConfiguration<WarehouseTransaction>
    {
        public void Configure(EntityTypeBuilder<WarehouseTransaction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedBy).HasMaxLength(255).IsRequired();

            builder.Property(x => x.LastModifiedBy).HasMaxLength(255).IsRequired(false);

            builder.HasDiscriminator(x => x.TransactionType);

            builder.Property(e => e.TransactionType)
                .HasMaxLength(255)
                .HasColumnName("TransactionType");
        }
    }
}