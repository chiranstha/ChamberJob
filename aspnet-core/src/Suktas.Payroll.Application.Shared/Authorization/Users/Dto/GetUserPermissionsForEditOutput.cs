using System.Collections.Generic;
using Suktas.Payroll.Authorization.Permissions.Dto;

namespace Suktas.Payroll.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}