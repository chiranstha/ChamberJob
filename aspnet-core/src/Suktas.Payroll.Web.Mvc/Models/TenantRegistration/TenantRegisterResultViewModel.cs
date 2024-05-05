using Abp.AutoMapper;
using Suktas.Payroll.MultiTenancy.Dto;

namespace Suktas.Payroll.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(RegisterTenantOutput))]
    public class TenantRegisterResultViewModel : RegisterTenantOutput
    {
        public string TenantLoginAddress { get; set; }
    }
}