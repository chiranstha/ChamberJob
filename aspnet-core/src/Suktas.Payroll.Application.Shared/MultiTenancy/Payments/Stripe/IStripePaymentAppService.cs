using System.Threading.Tasks;
using Abp.Application.Services;
using Suktas.Payroll.MultiTenancy.Payments.Dto;
using Suktas.Payroll.MultiTenancy.Payments.Stripe.Dto;

namespace Suktas.Payroll.MultiTenancy.Payments.Stripe
{
    public interface IStripePaymentAppService : IApplicationService
    {
        Task ConfirmPayment(StripeConfirmPaymentInput input);

        StripeConfigurationDto GetConfiguration();

        Task<SubscriptionPaymentDto> GetPaymentAsync(StripeGetPaymentInput input);

        Task<string> CreatePaymentSession(StripeCreatePaymentSessionInput input);
    }
}