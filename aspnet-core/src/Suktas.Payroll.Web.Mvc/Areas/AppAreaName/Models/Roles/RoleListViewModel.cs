using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Suktas.Payroll.Authorization.Permissions.Dto;
using Suktas.Payroll.Web.Areas.AppAreaName.Models.Common;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Models.Roles
{
    public class RoleListViewModel : IPermissionsEditViewModel
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}