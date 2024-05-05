using Abp.AspNetCore.Mvc.ViewComponents;

namespace Suktas.Payroll.Web.Views
{
    public abstract class PayrollViewComponent : AbpViewComponent
    {
        protected PayrollViewComponent()
        {
            LocalizationSourceName = PayrollConsts.LocalizationSourceName;
        }
    }
}