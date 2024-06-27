using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace JazzClub.Models.DomainModels
{
    public class User
    {

        public int UserId { get; set; }

        [DisplayName("Rol")]
        public string Role_name { get; set; }

        [Range(0, 5, ErrorMessage = "Only status from 0 to 5")]
        [Required]
        public int Status { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
