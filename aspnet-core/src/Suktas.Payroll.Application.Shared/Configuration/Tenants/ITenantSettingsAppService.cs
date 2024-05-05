using System.Threading.Tasks;
using Abp.Application.Services;
using Suktas.Payroll.Configuration.Tenants.Dto;

namespace Suktas.Payroll.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearDarkLogo();
        
        Task ClearLightLogo();

        Task ClearCustomCss();
    }
}
