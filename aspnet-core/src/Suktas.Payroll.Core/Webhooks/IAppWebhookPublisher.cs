using System.Threading.Tasks;
using Suktas.Payroll.Authorization.Users;

namespace Suktas.Payroll.WebHooks
{
    public interface IAppWebhookPublisher
    {
        Task PublishTestWebhook();
    }
}
