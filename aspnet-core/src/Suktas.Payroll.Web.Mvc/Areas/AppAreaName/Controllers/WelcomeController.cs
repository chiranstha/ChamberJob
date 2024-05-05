using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suktas.Payroll.Web.Controllers;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize]
    public class WelcomeController : PayrollControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}