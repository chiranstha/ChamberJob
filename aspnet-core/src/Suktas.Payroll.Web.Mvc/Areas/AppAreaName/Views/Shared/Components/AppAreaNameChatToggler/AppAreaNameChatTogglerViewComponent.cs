using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Suktas.Payroll.Web.Areas.AppAreaName.Models.Layout;
using Suktas.Payroll.Web.Views;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Views.Shared.Components.AppAreaNameChatToggler
{
    public class AppAreaNameChatTogglerViewComponent : PayrollViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(string cssClass, string iconClass = "flaticon-chat-2 fs-2")
        {
            return Task.FromResult<IViewComponentResult>(View(new ChatTogglerViewModel
            {
                CssClass = cssClass,
                IconClass = iconClass
            }));
        }
    }
}
