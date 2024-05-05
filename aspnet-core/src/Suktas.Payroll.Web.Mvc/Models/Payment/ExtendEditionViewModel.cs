using System.Collections.Generic;
using Suktas.Payroll.Editions.Dto;
using Suktas.Payroll.MultiTenancy.Payments;

namespace Suktas.Payroll.Web.Models.Payment
{
    public class ExtendEditionViewModel
    {
        public EditionSelectDto Edition { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}