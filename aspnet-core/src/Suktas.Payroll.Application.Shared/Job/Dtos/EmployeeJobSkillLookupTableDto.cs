using System;
using Abp.Application.Services.Dto;

namespace Suktas.Payroll.Job.Dtos
{
    public class EmployeeJobSkillLookupTableDto
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }
    }
}