using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockApp.Domain.Entities;

namespace StockApp.Infra.Data.EntityConfiguration
{
    public class SupplierEvaluationConfiguration : IEntityTypeConfiguration<SupplierEvaluation>
    {
        public void Configure(EntityTypeBuilder<SupplierEvaluation> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Category)
                .HasMaxLength(100)
                .IsRequired();
                
            builder.Property(e => e.Comment)
                .HasMaxLength(500)
                .IsRequired();
                
            builder.Property(e => e.EvaluatedBy)
                .HasMaxLength(100)
                .IsRequired();
                
            builder.HasOne(e => e.Supplier)
                .WithMany(s => s.Evaluations)
                .HasForeignKey(e => e.SupplierId);
        }
    }
}