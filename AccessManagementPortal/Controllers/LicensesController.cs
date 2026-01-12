using AccessManagementPortal.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccessManagementPortal.Controllers
{
    
    [Authorize(Roles="Admin")]
    public class LicensesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LicensesController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var licenses = await _db.Licenses
                .Include(l => l.Product)
                .Include(l => l.ApplicationUser)
                .OrderByDescending(l => l.Product)
                .ToListAsync();

            return View(licenses);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var License = await _db.Licenses.FindAsync(id);

            if (License == null)
            {
                return NotFound();
            }

            _db.Licenses.Remove(License);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");

        }


    }
}
