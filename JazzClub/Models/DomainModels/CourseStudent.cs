using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JazzClub.Models.DomainModels
{
	public class CourseStudent
	{
		
		public int Id { get; set; }
		public int StudentId { get; set; }

		public int CourseId { get; set; }

		[Display(Name = "Costo")]
		[DataType(DataType.Currency)]
		[Column(TypeName = "decimal(18, 2)")]
		public decimal Cost { get; set; }

		public string Notes { get; set; } = string.Empty;

		[Range(0, 5, ErrorMessage = "Only status from 0 to 5")]
		[Required]
		public int status { get; set; }


		public DateTime CreatedAt { get; set; }

	}
}
