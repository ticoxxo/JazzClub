using JazzClub.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JazzClub.Models.Configuration
{
	public class GuardianConfig : IEntityTypeConfiguration<Guardian>
	{
		public void Configure(EntityTypeBuilder<Guardian> entity)
		{
			entity.Property(entity => entity.CreatedAt)
				.HasDefaultValueSql("CURRENT_TIMESTAMP");
		}
	}
}
