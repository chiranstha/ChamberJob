using Abp.AutoMapper;
using Suktas.Payroll.Sessions.Dto;

namespace Suktas.Payroll.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}