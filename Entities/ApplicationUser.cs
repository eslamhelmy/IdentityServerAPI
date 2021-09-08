using Microsoft.AspNetCore.Identity;

namespace UserIdentity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsApproved { get; set; }
    }
}