using Microsoft.AspNetCore.Antiforgery;

namespace Suktas.Payroll.Web.Controllers
{
    public class AntiForgeryController : PayrollControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
