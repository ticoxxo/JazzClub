using JazzClub.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JazzClub.Models.Configuration
{
	public class CourseConfig : IEntityTypeConfiguration<Course>
	{
		public void Configure(EntityTypeBuilder<Course> entity)
		{
			entity
			   .Property(b => b.CreatedAt)
			   .HasDefaultValueSql("CURRENT_TIMESTAMP");
		}
	}
}
