using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YallaPharm.Domain.Entities;

namespace YallaPharm.Infrastructure.Data.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("clients");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.FullName).HasColumnName("full_name").HasMaxLength(30).IsRequired();
        builder.Property(e => e.Street).HasColumnName("street").HasMaxLength(500);
        builder.Property(e => e.Landmark).HasColumnName("landmark").HasMaxLength(500);
        builder.Property(e => e.GeolocationOfClientAddress).HasColumnName("geolocation_of_client_address").HasMaxLength(255);
        builder.Property(e => e.PhoneNumber).HasColumnName("phone_number").HasMaxLength(20).IsRequired();
        builder.Property(e => e.Age).HasColumnName("age");
        builder.Property(e => e.SocialUsername).HasColumnName("social_username").HasMaxLength(100);
        builder.Property(e => e.Language).HasColumnName("language").HasMaxLength(10).HasConversion<string>();
        builder.Property(e => e.HavingChildren).HasColumnName("having_children").HasDefaultValue(false);
        builder.Property(e => e.HavingElderly).HasColumnName("having_elderly").HasDefaultValue(false);
        builder.Property(e => e.Gender).HasColumnName("gender").HasMaxLength(20).HasConversion<string>();
        builder.Property(e => e.FamilyStatus).HasColumnName("family_status").HasMaxLength(30).HasConversion<string>();
        builder.Property(e => e.EconomicStanding).HasColumnName("economic_standing").HasMaxLength(30).HasConversion<string>();
        builder.Property(e => e.ChildrensAge).HasColumnName("childrens_age").HasMaxLength(30);
        builder.Property(e => e.Type).HasColumnName("type").HasMaxLength(30).HasConversion<string>();
        builder.Property(e => e.ContactType).HasColumnName("contact_type").HasMaxLength(30).HasConversion<string>();
        builder.Property(e => e.TakeIntoAccount).HasColumnName("take_into_account").HasMaxLength(255);
        builder.Property(e => e.DiscountForClient).HasColumnName("discount_for_client").HasDefaultValue(0);
    }
}
