using Abp.Application.Services.Dto;
using System;

namespace Suktas.Payroll.Job.Dtos
{
    public class GetAllJobDemandsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string CompanyNameFilter { get; set; }

        public string JobSkillNameFilter { get; set; }

    }
}