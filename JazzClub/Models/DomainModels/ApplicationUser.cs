using Microsoft.AspNetCore.Identity;

namespace JazzClub.Models.DomainModels
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Student>? students { get; set; }
    }
}
