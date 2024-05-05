using Abp.Application.Services.Dto;

namespace Suktas.Payroll.Notifications.Dto
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}