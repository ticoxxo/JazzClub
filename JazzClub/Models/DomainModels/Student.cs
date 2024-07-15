using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//  TODO: Add column photo to database and clean the column names 

namespace JazzClub.Models.DomainModels
{
	public class Student
	{

		public Student()
		{
			Courses = new List<Course>();
          
		}


		public int Id { get; set; }
		[Display(Name = "First Name")]
		[StringLength(100, ErrorMessage = "First name may not exceed 100 characters")]
		[Required]
		public string FirstName { get; set; } = string.Empty;

		[Display(Name = "Last Name")]
		[StringLength(100, ErrorMessage = "First name may not exceed 100 characters")]
		[Required]
		public string LastName { get; set; } = string.Empty;

		[DisplayName("Date of birth")]
		[DataType(DataType.Date)]
		//[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? Birthday { get; set; }

		[Phone]
		public string? Cellphone { get; set; } = string.Empty;

		[EmailAddress]
		public string? Email { get; set; } = string.Empty;

		public string? Address { get; set; }

		[DisplayName("# Ext")]
		public string? No_ext { get; set; }

		[DisplayName("# Int")]
		public string? No_int { get; set; }

		[DisplayName("Notas de salud")]
		public string? HealthNotes { get; set; }

		public int? GuardianId { get; set; }
		[ValidateNever]
		public Guardian Guardian { get; set; }

		[DisplayName("Started studying")]
		[DataType(DataType.Date)]
		//[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? Start_studying_at { get; set; }

		[Range(0, 5, ErrorMessage = "Only status from 0 to 5")]
		[Required]
		public int Status { get; set; }

		[DisplayName("Created by")]
		public int? Created_by { get; set; }

		public DateTime CreatedAt { get; set; }

		[DisplayName("Last update by")]
		public int? Last_updated_by { get; set; }

		[ValidateNever]
		public byte[]? Photo { get; set; }

		// Read only display property
		public string FullName => $"{FirstName} {LastName}";

		public List<Course> Courses { get; set; }

		public ICollection<Fingertip> Fingertips { get; } = new List<Fingertip>();

		[NotMapped]
		[ValidateNever]
		public List<SelectListItem> GuardiansSelectList { get; set; } = new();

		[NotMapped]
		[ValidateNever]
		public Payment? LastPayment { get; set; }
	}

}

