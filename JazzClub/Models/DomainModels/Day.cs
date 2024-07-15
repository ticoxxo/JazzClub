using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JazzClub.Models.DomainModels
{
	public class Day
	{
		public Day()
		{
			Courses = new List<Course>();
		}

		public int DayId { get; set; }

		[StringLength(10)]
		[Required]
		public string Name { get; set; } = string.Empty;

		public List<Course> Courses { get; set; }

		[NotMapped]
		public bool IsChecked { get; set; } = false;
	}
}
