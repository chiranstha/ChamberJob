using Abp.AutoMapper;
using Suktas.Payroll.MultiTenancy.Dto;

namespace Suktas.Payroll.Mobile.MAUI.Models.Tenants
{
    [AutoMapFrom(typeof(TenantListDto))]
    [AutoMapTo(typeof(TenantEditDto), typeof(CreateTenantInput))]
    public class TenantListModel : TenantListDto
    {
 
    }
}
