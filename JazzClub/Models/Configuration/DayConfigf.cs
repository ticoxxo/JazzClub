using JazzClub.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JazzClub.Models.Configuration
{
	public class DayConfigf : IEntityTypeConfiguration<Day>
	{
		public void Configure(EntityTypeBuilder<Day> entity)
		{
			
			entity.HasData(
				new Day { DayId = 1, Name = "Lunes" },
				new Day { DayId = 2, Name = "Martes" },
				new Day { DayId = 3, Name = "Miercoles" },
				new Day { DayId = 4, Name = "Jueves" },
				new Day { DayId = 5, Name = "Viernes" },
				new Day { DayId = 6, Name = "Sábado" },
				new Day { DayId = 7, Name = "Domingo" }
				);
		}
	}
}
