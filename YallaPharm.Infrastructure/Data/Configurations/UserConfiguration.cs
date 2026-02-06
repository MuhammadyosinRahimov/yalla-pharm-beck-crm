using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YallaPharm.Domain.Entities;

namespace YallaPharm.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.FirstName).HasColumnName("first_name").HasMaxLength(30).IsRequired();
        builder.Property(e => e.LastName).HasColumnName("last_name").HasMaxLength(30).IsRequired();
        builder.Property(e => e.PhoneNumber).HasColumnName("phone_number").HasMaxLength(20).IsRequired();
        builder.Property(e => e.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
        builder.Property(e => e.Password).HasColumnName("password").HasMaxLength(200).IsRequired();
        builder.Property(e => e.Role).HasColumnName("role").HasMaxLength(20).HasConversion<string>().IsRequired();
    }
}
