using AccessManagementPortal.Models;

namespace AccessManagementPortal.ViewModels
{
    public class LicenseIndexVm
    {
        public List<License> Licenses { get; set; } = new List<License>();
        public List<Product> Products { get; set; } = new List<Product>();


        public bool? IsActive { get; set; }
        public int? ProductId { get; set; }
    }
}
