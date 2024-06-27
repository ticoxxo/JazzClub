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

        [Range(0, 5, ErrorMessage = "Only status from 0 to 5")]
        [Required]
        public int Status { get; set; }

        [Display(Name= "Costo")]
        [DataType(DataType.Currency)]
		[Column(TypeName = "decimal(18, 2)")]
        //[DataType(DataType.Currency)] 
        [Required(ErrorMessage ="Please enter a cost for the class.")]
        public decimal Cost { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<Student> Students { get; set; }

        public List<Day> Days { get; set; } 

	}
}
