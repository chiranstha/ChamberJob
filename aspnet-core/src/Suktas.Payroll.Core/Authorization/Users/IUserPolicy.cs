using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace Suktas.Payroll.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
