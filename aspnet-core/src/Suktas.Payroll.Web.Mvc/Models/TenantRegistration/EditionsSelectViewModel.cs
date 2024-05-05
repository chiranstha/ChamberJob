using Abp.AutoMapper;
using Suktas.Payroll.MultiTenancy.Dto;

namespace Suktas.Payroll.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(EditionsSelectOutput))]
    public class EditionsSelectViewModel : EditionsSelectOutput
    {
    }
}
