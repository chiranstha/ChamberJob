using System.Threading.Tasks;
using Suktas.Payroll.Sessions.Dto;

namespace Suktas.Payroll.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
