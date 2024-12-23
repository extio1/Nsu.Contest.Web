namespace Nsu.Contest.Web.HRManager.Model.Data.EntityConfiguration;

using Nsu.Contest.Web.Common.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        builder.ToTable("Wishlist");

        builder.HasKey(w => w.Id);

        builder.Property(w => new{w.Id, w.HackatonId})
               .HasColumnName("Id");

        builder.HasOne(w => w.ForEmployee)
               .WithMany()
               .HasForeignKey(w => w.ForEmployeeId);
    }
}

