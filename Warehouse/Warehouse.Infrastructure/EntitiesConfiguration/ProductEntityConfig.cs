﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.EntitiesConfiguration
{
    public class ProductEntityConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(255);
            builder.Property(x => x.CreatedBy).HasMaxLength(255).IsRequired();
            builder.Property(x => x.LastModifiedBy).HasMaxLength(255).IsRequired(false);
        }
    }
}