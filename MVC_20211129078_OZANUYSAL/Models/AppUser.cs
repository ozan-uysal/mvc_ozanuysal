using Microsoft.AspNetCore.Identity;

namespace MVC_20211129078_OZANUYSAL.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
