using System.Threading.Tasks;
using Abp.Webhooks;

namespace Suktas.Payroll.WebHooks
{
    public interface IWebhookEventAppService
    {
        Task<WebhookEvent> Get(string id);
    }
}
