using System.Collections.Generic;
using Suktas.Payroll.Authorization.Permissions.Dto;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}