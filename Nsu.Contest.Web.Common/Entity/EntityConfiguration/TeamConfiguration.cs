namespace Nsu.Contest.Web.Common.Entity.EntityConfiguration;

using Nsu.Contest.Web.Common.Entity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TeamConfiguration: IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("Team");

        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Id)
                .HasColumnName("Id");

        builder.HasOne(t => t.Teamlead).WithMany();
        builder.HasOne(t => t.Junior).WithMany();
    }
}
