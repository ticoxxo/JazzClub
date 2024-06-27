using JazzClub.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JazzClub.Models.Configuration
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> entity)
        {
            entity.HasMany(e => e.Courses)
                .WithMany(d => d.Students)
                .UsingEntity<CourseStudent>();

            entity.Property(a => a.GuardianId)
                .IsRequired(false);

            entity.Property(a => a.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }

    }
}
