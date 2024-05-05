using Abp.Authorization;
using Suktas.Payroll.Authorization.Roles;
using Suktas.Payroll.Authorization.Users;

namespace Suktas.Payroll.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
