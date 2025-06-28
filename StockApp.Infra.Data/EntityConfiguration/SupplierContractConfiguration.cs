using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockApp.Domain.Entities;

namespace StockApp.Infra.Data.EntityConfiguration
{
    public class SupplierContractConfiguration : IEntityTypeConfiguration<SupplierContract>
    {
        public void Configure(EntityTypeBuilder<SupplierContract> builder)
        {
            builder.HasKey(c => c.Id);
            
            builder.Property(c => c.ContractNumber)
                .HasMaxLength(50)
                .IsRequired();
                
            builder.Property(c => c.Description)
                .HasMaxLength(500)
                .IsRequired();
                
            builder.Property(c => c.Value)
                .HasPrecision(18, 2);
                
            builder.HasOne(c => c.Supplier)
                .WithMany(s => s.Contracts)
                .HasForeignKey(c => c.SupplierId);
        }
    }
}