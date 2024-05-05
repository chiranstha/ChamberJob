using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Suktas.Payroll.Web.Areas.AppAreaName.Models.Layout;
using Suktas.Payroll.Web.Session;
using Suktas.Payroll.Web.Views;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Views.Shared.Themes.Theme10.Components.AppAreaNameTheme10Brand
{
    public class AppAreaNameTheme10BrandViewComponent : PayrollViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppAreaNameTheme10BrandViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerModel = new HeaderViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(headerModel);
        }
    }
}
