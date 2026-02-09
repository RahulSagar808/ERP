using Microsoft.AspNetCore.Identity;

namespace ERP.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisteredOn { get; set; }
        public string ProfilePictureUrl { get; set; } = "";
    }
}
