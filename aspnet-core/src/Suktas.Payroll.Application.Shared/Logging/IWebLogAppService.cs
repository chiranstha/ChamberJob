using Abp.Application.Services;
using Suktas.Payroll.Dto;
using Suktas.Payroll.Logging.Dto;

namespace Suktas.Payroll.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
