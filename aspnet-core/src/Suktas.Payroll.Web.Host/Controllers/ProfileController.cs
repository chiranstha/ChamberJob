using Abp.AspNetCore.Mvc.Authorization;
using Suktas.Payroll.Authorization.Users.Profile;
using Suktas.Payroll.Graphics;
using Suktas.Payroll.Storage;

namespace Suktas.Payroll.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ProfileController : ProfileControllerBase
    {
        public ProfileController(
            ITempFileCacheManager tempFileCacheManager,
            IProfileAppService profileAppService,
            IImageValidator imageValidator) :
            base(tempFileCacheManager, profileAppService, imageValidator)
        {
        }
    }
}