using System.Collections.Generic;
using Suktas.Payroll.Auditing.Dto;
using Suktas.Payroll.Dto;

namespace Suktas.Payroll.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}
