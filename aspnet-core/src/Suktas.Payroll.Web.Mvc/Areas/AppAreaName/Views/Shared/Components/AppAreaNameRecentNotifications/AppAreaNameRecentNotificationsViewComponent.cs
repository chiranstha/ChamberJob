using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Suktas.Payroll.Web.Areas.AppAreaName.Models.Layout;
using Suktas.Payroll.Web.Views;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Views.Shared.Components.AppAreaNameRecentNotifications
{
    public class AppAreaNameRecentNotificationsViewComponent : PayrollViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(string cssClass, string iconClass = "flaticon-alert-2 unread-notification fs-2")
        {
            var model = new RecentNotificationsViewModel
            {
                CssClass = cssClass,
                IconClass = iconClass
            };
            
            return Task.FromResult<IViewComponentResult>(View(model));
        }
    }
}
