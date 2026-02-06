using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YallaPharm.Domain.Entities;

namespace YallaPharm.Infrastructure.Data.Configurations;

public class PharmacyContactConfiguration : IEntityTypeConfiguration<PharmacyContact>
{
    public void Configure(EntityTypeBuilder<PharmacyContact> builder)
    {
        builder.ToTable("pharmacy_contacts");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.PharmacyId).HasColumnName("pharmacy_id").HasMaxLength(36).IsRequired();
        builder.Property(e => e.Contact).HasColumnName("contact").HasMaxLength(100).IsRequired();
        builder.Property(e => e.ContactType).HasColumnName("contact_type").HasMaxLength(30).HasConversion<string>();

        builder.HasOne(e => e.Pharmacy)
            .WithMany(p => p.PharmacyContacts)
            .HasForeignKey(e => e.PharmacyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
