using Abp.Events.Bus;

namespace Suktas.Payroll.MultiTenancy
{
    public class RecurringPaymentsEnabledEventData : EventData
    {
        public int TenantId { get; set; }
    }
}