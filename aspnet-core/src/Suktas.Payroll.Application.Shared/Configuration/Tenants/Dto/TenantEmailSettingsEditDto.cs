using Abp.Auditing;
using Suktas.Payroll.Configuration.Dto;

namespace Suktas.Payroll.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}