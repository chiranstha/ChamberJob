using Abp.Application.Services.Dto;
using System;

namespace Suktas.Payroll.Job.Dtos
{
    public class GetAllEmploymentsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public int? MaxMaleFilter { get; set; }
        public int? MinMaleFilter { get; set; }

        public string CompanyNameFilter { get; set; }

    }
}