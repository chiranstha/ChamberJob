using Abp.AutoMapper;
using Suktas.Payroll.Authorization.Users;
using Suktas.Payroll.Authorization.Users.Dto;
using Suktas.Payroll.Web.Areas.AppAreaName.Models.Common;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; set; }
    }
}