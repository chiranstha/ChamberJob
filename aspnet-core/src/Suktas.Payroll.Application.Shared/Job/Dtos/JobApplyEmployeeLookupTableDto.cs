using System;
using Abp.Application.Services.Dto;

namespace Suktas.Payroll.Job.Dtos
{
    public class JobApplyEmployeeLookupTableDto
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }
    }
}