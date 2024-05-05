using System.Threading.Tasks;
using Abp.Application.Services;
using Suktas.Payroll.Sessions.Dto;

namespace Suktas.Payroll.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
