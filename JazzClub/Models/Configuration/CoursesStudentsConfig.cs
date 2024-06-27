using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JazzClub.Models.DomainModels;

namespace JazzClub.Models.Configuration
{
    public class CoursesStudentsConfig : IEntityTypeConfiguration<CourseStudent>
    {
        public void Configure(EntityTypeBuilder<CourseStudent> entity)
        {
            entity.Property(a => a.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
