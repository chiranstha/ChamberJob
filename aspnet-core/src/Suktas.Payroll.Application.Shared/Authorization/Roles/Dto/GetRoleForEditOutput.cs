using System.Collections.Generic;
using Suktas.Payroll.Authorization.Permissions.Dto;

namespace Suktas.Payroll.Authorization.Roles.Dto
{
    public class GetRoleForEditOutput
    {
        public RoleEditDto Role { get; set; }

        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}