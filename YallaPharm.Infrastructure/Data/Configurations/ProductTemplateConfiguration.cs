using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YallaPharm.Domain.Entities;

namespace YallaPharm.Infrastructure.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        builder.Property(e => e.ParentId).HasColumnName("parent_id").HasMaxLength(36);

        builder.HasOne(e => e.Parent)
            .WithMany(c => c.Children)
            .HasForeignKey(e => e.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class ForWhomConfiguration : IEntityTypeConfiguration<ForWhom>
{
    public void Configure(EntityTypeBuilder<ForWhom> builder)
    {
        builder.ToTable("for_whom");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.ForWhomValue).HasColumnName("for_whom").HasMaxLength(100).IsRequired();
    }
}

public class ApplicationMethodConfiguration : IEntityTypeConfiguration<ApplicationMethod>
{
    public void Configure(EntityTypeBuilder<ApplicationMethod> builder)
    {
        builder.ToTable("application_methods");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.ApplicationMethodValue).HasColumnName("application_method").HasMaxLength(100).IsRequired();
    }
}

public class OrgansAndSystemConfiguration : IEntityTypeConfiguration<OrgansAndSystem>
{
    public void Configure(EntityTypeBuilder<OrgansAndSystem> builder)
    {
        builder.ToTable("organs_and_systems");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.OrgansAndSystemValue).HasColumnName("organs_and_system").HasMaxLength(100).IsRequired();
    }
}

public class PackagingMaterialConfiguration : IEntityTypeConfiguration<PackagingMaterial>
{
    public void Configure(EntityTypeBuilder<PackagingMaterial> builder)
    {
        builder.ToTable("packaging_materials");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.PackagingMaterialValue).HasColumnName("packaging_material").HasMaxLength(100).IsRequired();
    }
}

public class PreparationColorConfiguration : IEntityTypeConfiguration<PreparationColor>
{
    public void Configure(EntityTypeBuilder<PreparationColor> builder)
    {
        builder.ToTable("preparation_colors");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.PreparationColorValue).HasColumnName("preparation_color").HasMaxLength(100).IsRequired();
    }
}

public class PreparationMaterialConfiguration : IEntityTypeConfiguration<PreparationMaterial>
{
    public void Configure(EntityTypeBuilder<PreparationMaterial> builder)
    {
        builder.ToTable("preparation_materials");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.PreparationMaterialValue).HasColumnName("preparation_material").HasMaxLength(100).IsRequired();
    }
}

public class ScopeOfApplicationConfiguration : IEntityTypeConfiguration<ScopeOfApplication>
{
    public void Configure(EntityTypeBuilder<ScopeOfApplication> builder)
    {
        builder.ToTable("scope_of_applications");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.ScopeOfApplicationValue).HasColumnName("scope_of_application").HasMaxLength(100).IsRequired();
    }
}

public class TimeOfApplicationConfiguration : IEntityTypeConfiguration<TimeOfApplication>
{
    public void Configure(EntityTypeBuilder<TimeOfApplication> builder)
    {
        builder.ToTable("time_of_applications");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.TimeOfApplicationValue).HasColumnName("time_of_application").HasMaxLength(100).IsRequired();
    }
}

public class ProductTemplateConfiguration : IEntityTypeConfiguration<ProductTemplate>
{
    public void Configure(EntityTypeBuilder<ProductTemplate> builder)
    {
        builder.ToTable("product_templates");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(e => e.CategoryId).HasColumnName("category_id").HasMaxLength(36).IsRequired();
        builder.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
        builder.Property(e => e.Nickname).HasColumnName("nickname").HasMaxLength(100);
        builder.Property(e => e.ActiveIngredient).HasColumnName("active_ingredient").HasMaxLength(100);
        builder.Property(e => e.Dosage).HasColumnName("dosage").HasMaxLength(500);
        builder.Property(e => e.DosageUnit).HasColumnName("dosage_unit").HasMaxLength(30);
        builder.Property(e => e.MinimumQuantityPerPiece).HasColumnName("minimum_quantity_per_piece");
        builder.Property(e => e.PackQuantityOrDrugVolume).HasColumnName("pack_quantity_or_drug_volume").HasMaxLength(500).IsRequired();
        builder.Property(e => e.PackagingUnit).HasColumnName("packaging_unit").HasMaxLength(30);
        builder.Property(e => e.PreparationTaste).HasColumnName("preparation_taste").HasMaxLength(50);
        builder.Property(e => e.AgeFrom).HasColumnName("age_from");
        builder.Property(e => e.AgeTo).HasColumnName("age_to");
        builder.Property(e => e.Instructions).HasColumnName("instructions").HasMaxLength(1000);
        builder.Property(e => e.IndicationForUse).HasColumnName("indication_for_use").HasMaxLength(1000).IsRequired();
        builder.Property(e => e.ContraindicationForUse).HasColumnName("contraindication_for_use").HasMaxLength(1000).IsRequired();
        builder.Property(e => e.Symptom).HasColumnName("symptom").HasMaxLength(1000);
        builder.Property(e => e.ForAllergySufferers).HasColumnName("for_allergy_sufferers").HasMaxLength(30);
        builder.Property(e => e.ForDiabetics).HasColumnName("for_diabetics").HasMaxLength(30);
        builder.Property(e => e.ForPregnantWomen).HasColumnName("for_pregnant_women").HasMaxLength(30);
        builder.Property(e => e.ForChildren).HasColumnName("for_children").HasMaxLength(30);
        builder.Property(e => e.ForDriver).HasColumnName("for_driver").HasMaxLength(30);
        builder.Property(e => e.SeasonOfApplication).HasColumnName("season_of_application").HasMaxLength(30);
        builder.Property(e => e.DriedFruit).HasColumnName("dried_fruit").HasMaxLength(100);
        builder.Property(e => e.Comment).HasColumnName("comment").HasMaxLength(5000);
        builder.Property(e => e.WithCaution).HasColumnName("with_caution").HasMaxLength(1000);
        builder.Property(e => e.VacationCondition).HasColumnName("vacation_condition").HasMaxLength(30);
        builder.Property(e => e.ForWhomId).HasColumnName("for_whom_id").HasMaxLength(36).IsRequired();
        builder.Property(e => e.ApplicationMethodId).HasColumnName("application_method_id").HasMaxLength(36).IsRequired();
        builder.Property(e => e.OrgansAndSystemsId).HasColumnName("organs_and_systems_id").HasMaxLength(36);
        builder.Property(e => e.PackagingMaterialId).HasColumnName("packaging_material_id").HasMaxLength(36).IsRequired();
        builder.Property(e => e.PreparationColorId).HasColumnName("preparation_color_id").HasMaxLength(36).IsRequired();
        builder.Property(e => e.PreparationMaterialId).HasColumnName("preparation_material_id").HasMaxLength(36).IsRequired();
        builder.Property(e => e.ScopeOfApplicationId).HasColumnName("scope_of_application_id").HasMaxLength(36);
        builder.Property(e => e.TimeOfApplicationId).HasColumnName("time_of_application_id").HasMaxLength(36).IsRequired();

        builder.HasOne(e => e.Category)
            .WithMany(c => c.ProductTemplates)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.ForWhom)
            .WithMany(f => f.ProductTemplates)
            .HasForeignKey(e => e.ForWhomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.ApplicationMethod)
            .WithMany(a => a.ProductTemplates)
            .HasForeignKey(e => e.ApplicationMethodId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.OrgansAndSystems)
            .WithMany(o => o.ProductTemplates)
            .HasForeignKey(e => e.OrgansAndSystemsId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.PackagingMaterial)
            .WithMany(p => p.ProductTemplates)
            .HasForeignKey(e => e.PackagingMaterialId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.PreparationColor)
            .WithMany(p => p.ProductTemplates)
            .HasForeignKey(e => e.PreparationColorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.PreparationMaterial)
            .WithMany(p => p.ProductTemplates)
            .HasForeignKey(e => e.PreparationMaterialId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.ScopeOfApplication)
            .WithMany(s => s.ProductTemplates)
            .HasForeignKey(e => e.ScopeOfApplicationId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(e => e.TimeOfApplication)
            .WithMany(t => t.ProductTemplates)
            .HasForeignKey(e => e.TimeOfApplicationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
