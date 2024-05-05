using Abp.AspNetCore.Mvc.Authorization;
using Suktas.Payroll.Authorization;
using Suktas.Payroll.Storage;
using Abp.BackgroundJobs;

namespace Suktas.Payroll.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}