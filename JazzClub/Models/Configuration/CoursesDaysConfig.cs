using JazzClub.Model.DomainsModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JazzClub.Models.Configuration
{
    public class CoursesDaysConfig : IEntityTypeConfiguration<CourseDay>
    {
        public void Configure(EntityTypeBuilder<CourseDay> entity)
        {
            entity.Property(a => a.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
