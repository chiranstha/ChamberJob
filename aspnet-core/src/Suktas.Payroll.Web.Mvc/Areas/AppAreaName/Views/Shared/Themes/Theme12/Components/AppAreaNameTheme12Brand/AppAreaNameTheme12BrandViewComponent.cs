using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Suktas.Payroll.Web.Areas.AppAreaName.Models.Layout;
using Suktas.Payroll.Web.Session;
using Suktas.Payroll.Web.Views;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Views.Shared.Themes.Theme12.Components.AppAreaNameTheme12Brand
{
    public class AppAreaNameTheme12BrandViewComponent : PayrollViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppAreaNameTheme12BrandViewComponent(IPerRequestSessionCache sessionCache)
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
