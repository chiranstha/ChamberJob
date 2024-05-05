using Abp.Application.Services.Dto;
using System;

namespace Suktas.Payroll.Job.Dtos
{
    public class GetAllEmployeeForExcelInput
    {
        public string Filter { get; set; }

        public string JobSkillNameFilter { get; set; }

    }
}