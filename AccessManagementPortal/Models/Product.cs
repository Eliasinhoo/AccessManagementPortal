using System.ComponentModel.DataAnnotations;

namespace AccessManagementPortal.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ICollection<License> Licenses { get; set; } = new List<License>();
    }
}


