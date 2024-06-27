using System.ComponentModel.DataAnnotations;

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
	}
}
