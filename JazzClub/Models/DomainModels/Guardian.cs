using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JazzClub.Models.DomainModels
{
	// NO SE USA , borrar despues
	public class Guardian
	{
		public int GuardianId { get; set; }

		[StringLength(100, ErrorMessage = "Name may not exceed 100 characters")]
		[Required]
		public string Name { get; set; } = string.Empty;

		[Phone]
		public string? Cellphone { get; set; } = string.Empty;
		[Phone]
		public string? Phone { get; set; } = string.Empty;

		[EmailAddress]
		public string? Email { get; set; } = string.Empty;

		public string? Address { get; set; }
		[DisplayName("# Ext")]

		public string? no_ext { get; set; }
		[DisplayName("# Int")]
		public string? no_int { get; set; }

		[DisplayName("Notes")]
		public string? notes { get; set; }


		[Range(0, 5, ErrorMessage = "Only status from 0 to 5")]
		[Required]
		public int status { get; set; }

		[DisplayName("Created By")]
		public int? created_by { get; set; }

		/*
        [DisplayName("Created at")]
        public DateTime created_at { get; set; } = DateTime.Now;
        */
		public DateTime CreatedAt { get; set; }

		/* public Guardian DeepCopy()
        {
            Guardian other = (Guardian)this.MemberwiseClone();
            other.Name =  new string(Name);
            other.Cellphone = new string(Cellphone);
            return other;
        }
        */
	}
}
