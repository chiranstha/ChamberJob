using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Suktas.Payroll.Web.Areas.AppAreaName.Models.Layout;
using Suktas.Payroll.Web.Views;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Views.Shared.Components.
    AppAreaNameQuickThemeSelect
{
    public class AppAreaNameQuickThemeSelectViewComponent : PayrollViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(string cssClass, string iconClass = "flaticon-interface-7 fs-2")
        {
            return Task.FromResult<IViewComponentResult>(View(new QuickThemeSelectionViewModel
            {
                CssClass = cssClass,
                IconClass = iconClass
            }));
        }
    }
}
