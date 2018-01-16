using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace esencialAdmin.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(256)]
        public string FirstName { get; set; }
        [MaxLength(256)]
        public string LastName { get; set; }
    }
}