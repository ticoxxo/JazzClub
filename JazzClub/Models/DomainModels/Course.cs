using JazzClub.Model.DomainsModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JazzClub.Models.DomainModels
{
	public class Course
	{
		public Course()
		{
			Students = new List<Student>();
			Days = new List<Day>();
            
        }

        public int CourseId { get; set; }
		[Required]
		public string Name { get; set; } = string.Empty;

		[Range(0, 5, ErrorMessage = "El estatus va de 0 a 5")]
		[Required]
		public int Status { get; set; }

		[Display(Name = "Costo")]
		[DataType(DataType.Currency)]
		[Column(TypeName = "decimal(18, 2)")]
		//[DataType(DataType.Currency)] 
		[Required(ErrorMessage = "Ingrese un costo para la clase")]
		public decimal Cost { get; set; }

		public DateTime CreatedAt { get; set; }

		public List<Student> Students { get; set; }

		public List<Day> Days { get; set; }

        [NotMapped]
		[ValidateNever]
		public List<SelectListItem> StudentsSelectList { get; set; } = new();
		[NotMapped]
		[ValidateNever]
		public int SelectedStudentId { get; set; }
	}
}
