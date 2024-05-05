using System.Collections.Generic;
using Abp.Auditing;

namespace Suktas.Payroll.Auditing
{
    public interface IExpiredAndDeletedAuditLogBackupService
    {
        bool CanBackup();
        
        void Backup(List<AuditLog> auditLogs);
    }
}