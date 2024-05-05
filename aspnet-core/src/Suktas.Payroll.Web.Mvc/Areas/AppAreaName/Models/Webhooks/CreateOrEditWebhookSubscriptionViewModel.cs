using Abp.Application.Services.Dto;
using Abp.Webhooks;
using Suktas.Payroll.WebHooks.Dto;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Models.Webhooks
{
    public class CreateOrEditWebhookSubscriptionViewModel
    {
        public WebhookSubscription WebhookSubscription { get; set; }

        public ListResultDto<GetAllAvailableWebhooksOutput> AvailableWebhookEvents { get; set; }
    }
}
