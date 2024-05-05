using Abp.AutoMapper;
using Suktas.Payroll.MultiTenancy;
using Suktas.Payroll.MultiTenancy.Dto;
using Suktas.Payroll.Web.Areas.AppAreaName.Models.Common;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }
    }
}