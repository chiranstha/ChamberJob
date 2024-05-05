using Abp.Application.Services.Dto;

namespace Suktas.Payroll.Job.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}