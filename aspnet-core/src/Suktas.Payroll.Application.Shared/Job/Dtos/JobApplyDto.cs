using System;
using Abp.Application.Services.Dto;

namespace Suktas.Payroll.Job.Dtos
{
    public class JobApplyDto : EntityDto<Guid>
    {
        public DateTime Date { get; set; }

        public int CompanyId { get; set; }

        public Guid JobDemandId { get; set; }

        public Guid EmployeeId { get; set; }

    }
}