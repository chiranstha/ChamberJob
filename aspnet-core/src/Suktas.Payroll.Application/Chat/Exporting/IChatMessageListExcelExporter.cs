using System.Collections.Generic;
using Abp;
using Suktas.Payroll.Chat.Dto;
using Suktas.Payroll.Dto;

namespace Suktas.Payroll.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}
