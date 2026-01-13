using AccessManagementPortal.Models;

namespace AccessManagementPortal.Services
{
    public record LicenseQuery(bool? isActive, int? productId);


    public interface ILicenseService
    {
        Task<List<License>> GetLicensesAsync(LicenseQuery query);
    }
}
