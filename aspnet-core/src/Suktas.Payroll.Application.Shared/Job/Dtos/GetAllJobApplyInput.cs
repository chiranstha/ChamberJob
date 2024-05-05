using Abp.Application.Services.Dto;
using System;

namespace Suktas.Payroll.Job.Dtos
{
    public class GetAllJobApplyInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string CompanyNameFilter { get; set; }

        public string JobDemandNameFilter { get; set; }

        public string EmployeeNameFilter { get; set; }

    }
}