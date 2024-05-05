using Microsoft.AspNetCore.Mvc;
using Suktas.Payroll.Web.Controllers;

namespace Suktas.Payroll.Web.Public.Controllers
{
    public class AboutController : PayrollControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}