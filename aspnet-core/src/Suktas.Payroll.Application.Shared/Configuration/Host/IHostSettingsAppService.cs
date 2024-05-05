using System.Threading.Tasks;
using Abp.Application.Services;
using Suktas.Payroll.Configuration.Host.Dto;

namespace Suktas.Payroll.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
