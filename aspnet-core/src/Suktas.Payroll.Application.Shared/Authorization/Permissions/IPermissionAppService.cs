using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Suktas.Payroll.Authorization.Permissions.Dto;

namespace Suktas.Payroll.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
