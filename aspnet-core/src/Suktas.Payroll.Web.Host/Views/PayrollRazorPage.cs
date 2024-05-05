using Abp.AspNetCore.Mvc.Views;

namespace Suktas.Payroll.Web.Views
{
    public abstract class PayrollRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected PayrollRazorPage()
        {
            LocalizationSourceName = PayrollConsts.LocalizationSourceName;
        }
    }
}
