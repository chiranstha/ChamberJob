using Abp.Application.Services.Dto;

namespace Suktas.Payroll.Master.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}