using AccessManagementPortal.Data;
using AccessManagementPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessManagementPortal.Services
{
    public class LicenseService : ILicenseService
    {
        private readonly ApplicationDbContext _db;

        public LicenseService(ApplicationDbContext db) => _db = db;

        public async Task<List<License>> GetLicensesAsync(LicenseQuery q)
        {
            var query = _db.Licenses
                .Include(l => l.Product)
                .Include(l => l.ApplicationUser)
                .AsQueryable();

            if (q.isActive.HasValue)
                query = query.Where(l => l.IsActive == q.isActive.Value);

            if (q.productId.HasValue)
                query = query.Where(l => l.ProductId == q.productId.Value);

            query = query.OrderByDescending(l => l.AssignedDate);

            return await query.ToListAsync();
        }
    }
}
