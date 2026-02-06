using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YallaPharm.Domain.Entities;

namespace YallaPharm.Infrastructure.Data.Configurations;

public class ClientChildrenConfiguration : IEntityTypeConfiguration<ClientChildren>
{
    public void Configure(EntityTypeBuilder<ClientChildren> builder)
    {
        builder.ToTable("client_childrens");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.ClientId).HasColumnName("client_id").HasMaxLength(36).IsRequired();
        builder.Property(e => e.FullName).HasColumnName("full_name").HasMaxLength(100).IsRequired();
        builder.Property(e => e.Age).HasColumnName("age");
        builder.Property(e => e.Gender).HasColumnName("gender").HasMaxLength(20).HasConversion<string>();

        builder.HasOne(e => e.Client)
            .WithMany(c => c.Childrens)
            .HasForeignKey(e => e.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ClientAdultConfiguration : IEntityTypeConfiguration<ClientAdult>
{
    public void Configure(EntityTypeBuilder<ClientAdult> builder)
    {
        builder.ToTable("client_adults");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.ClientId).HasColumnName("client_id").HasMaxLength(36).IsRequired();
        builder.Property(e => e.FullName).HasColumnName("full_name").HasMaxLength(100).IsRequired();
        builder.Property(e => e.Age).HasColumnName("age");
        builder.Property(e => e.Gender).HasColumnName("gender").HasMaxLength(20).HasConversion<string>();

        builder.HasOne(e => e.Client)
            .WithMany(c => c.Adults)
            .HasForeignKey(e => e.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
