using System.ComponentModel.DataAnnotations;

namespace AccessManagementPortal.Models
{
    public class License
    {
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }


        public LicenseType LicenseType { get; set; }



        public bool IsActive { get; set; }

        public DateTime AssignedDate { get; set; }

        public DateTime? ExpiryDate { get; set; }
    }
}
