using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace AccessManagementPortal.Models
{

    public enum LicenseType
    {
        Free,
        Standard,
        Enterprise
    }


    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public virtual ICollection<License> Licenses { get; set; } = new List<License>();
    }
}
