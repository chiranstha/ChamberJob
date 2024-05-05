using System.Collections.Generic;
using Suktas.Payroll.Editions;
using Suktas.Payroll.Editions.Dto;
using Suktas.Payroll.MultiTenancy.Payments;
using Suktas.Payroll.MultiTenancy.Payments.Dto;

namespace Suktas.Payroll.Web.Models.Payment
{
    public class BuyEditionViewModel
    {
        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public decimal? AdditionalPrice { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}
