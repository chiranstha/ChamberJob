using Suktas.Payroll.Editions;
using Suktas.Payroll.Editions.Dto;
using Suktas.Payroll.MultiTenancy.Payments;
using Suktas.Payroll.Security;
using Suktas.Payroll.MultiTenancy.Payments.Dto;

namespace Suktas.Payroll.Web.Models.TenantRegistration
{
    public class TenantRegisterViewModel
    {
        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public int? EditionId { get; set; }

        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }
    }
}
