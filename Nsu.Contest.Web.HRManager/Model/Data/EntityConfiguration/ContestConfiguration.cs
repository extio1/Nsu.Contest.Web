// namespace Nsu.Contest.Web.HRManager.Model.Data.EntityConfiguration;

// using Nsu.Contest.Web.Common.Entity;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// public class ContestConfiguration : IEntityTypeConfiguration<Contest>
// {
//     public void Configure(EntityTypeBuilder<Contest> builder)
//     {
//        builder.ToTable("Contest");

//        builder.HasKey(c => c.Id);
//        builder.Property(c => c.Id).HasColumnName("Id");
//        builder.Property(c => c.Score)
//               .HasColumnName("Points")
//               .HasDefaultValue(0);

//        builder.HasMany(c => c.Teamleads)
//               .WithMany();

//        builder.HasMany(c => c.Juniors)
//               .WithMany();

//        builder.HasMany(c => c.Teams)
//               .WithOne();
//     }
// }
