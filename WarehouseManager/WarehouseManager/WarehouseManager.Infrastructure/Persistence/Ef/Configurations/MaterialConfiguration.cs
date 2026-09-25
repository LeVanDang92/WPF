using Microsoft.EntityFrameworkCore;
using WarehouseManager.Domain.Entities;

namespace WarehouseManager.Infrastructure.Persistence.Ef.Configurations
{
    internal sealed class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Material> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(m => m.Code).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => x.Code).IsUnique();

            builder.Property(m => m.Name).IsRequired().HasMaxLength(200);
            builder.Property(m => m.Unit).IsRequired().HasMaxLength(20);

            builder.Property(x => x.IsActive).IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasPrecision(3)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .HasPrecision(3);
        }
    }
}
