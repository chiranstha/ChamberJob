using Abp.AspNetCore.Mvc.ViewComponents;

namespace Suktas.Payroll.Web.Public.Views
{
    public abstract class PayrollViewComponent : AbpViewComponent
    {
        protected PayrollViewComponent()
        {
            LocalizationSourceName = PayrollConsts.LocalizationSourceName;
        }
    }
}