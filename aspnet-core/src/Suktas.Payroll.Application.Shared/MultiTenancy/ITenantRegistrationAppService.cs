using System.Threading.Tasks;
using Abp.Application.Services;
using Suktas.Payroll.Editions.Dto;
using Suktas.Payroll.MultiTenancy.Dto;

namespace Suktas.Payroll.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}