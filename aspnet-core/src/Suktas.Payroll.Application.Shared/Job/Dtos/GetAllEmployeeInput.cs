using Abp.Application.Services.Dto;
using System;

namespace Suktas.Payroll.Job.Dtos
{
    public class GetAllEmployeeInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string JobSkillNameFilter { get; set; }

    }
}