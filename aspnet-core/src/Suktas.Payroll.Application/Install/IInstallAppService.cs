using System.Threading.Tasks;
using Abp.Application.Services;
using Suktas.Payroll.Install.Dto;

namespace Suktas.Payroll.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}