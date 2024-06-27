using JazzClub.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JazzClub.Models.Configuration
{
    public class FingerprintConfig : IEntityTypeConfiguration<Fingertip>
    {
        public void Configure(EntityTypeBuilder<Fingertip> entity)
        {
            entity.Property(a => a.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
