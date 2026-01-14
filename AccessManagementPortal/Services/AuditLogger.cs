
using AccessManagementPortal.Data;
using AccessManagementPortal.Models;

namespace AccessManagementPortal.Services
{
    public class AuditLogger : IAuditLogger
    {
    
        private readonly ApplicationDbContext _db;


        public AuditLogger(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task LogAsync(string action, string entityType, int entityId, string? actorUserId = null, string? actorEmail = null)
            {

            var log = new AuditLog
            {
                Action = action,
                EntityType = entityType,
                EntityId = entityId,
                ActorUserId = actorUserId,
                ActorEmail = actorEmail

            };

            _db.AuditLogs.Add(log);
            await _db.SaveChangesAsync();
                
        }
    }
}
