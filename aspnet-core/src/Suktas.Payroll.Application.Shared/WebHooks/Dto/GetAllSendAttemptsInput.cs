using Suktas.Payroll.Dto;

namespace Suktas.Payroll.WebHooks.Dto
{
    public class GetAllSendAttemptsInput : PagedInputDto
    {
        public string SubscriptionId { get; set; }
    }
}
