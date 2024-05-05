using Suktas.Payroll.MultiTenancy.Payments;

namespace Suktas.Payroll.Web.Models.Payment
{
    public class CancelPaymentModel
    {
        public string PaymentId { get; set; }

        public SubscriptionPaymentGatewayType Gateway { get; set; }
    }
}