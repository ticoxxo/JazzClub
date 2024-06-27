using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JazzClub.Models.DomainModels
{
    public class Fingertip
    {
        public int FingertipId { get; set; }

        public int StudentId { get; set; }
        [ValidateNever]
        public Student Student { get; set; } = null!;

        [DisplayName("Fingerprint")]
        [Required]
        public string code { get; set; } = null!;

        [Range(0, 5, ErrorMessage = "Only status from 0 to 5")]
        [Required]
        public int status { get; set; }

        [DisplayName("Created by")]
        public int? Created_by { get; set; }

        public DateTime CreatedAt { get; set; }

        [DisplayName("Last update by")]
        public int? Last_updated_by { get; set; }
    }
}
