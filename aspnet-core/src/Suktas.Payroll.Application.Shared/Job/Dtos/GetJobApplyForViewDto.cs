using Abp.Application.Services.Dto;
using System;

namespace Suktas.Payroll.Job.Dtos
{
    public class GetJobApplyForViewDto : EntityDto<Guid>
    {
        public DateTime Date { get; set; }

        public int CompanyId { get; set; }

        public Guid JobDemandId { get; set; }

        public Guid EmployeeId { get; set; }

        public string CompanyName { get; set; }

        public string JobDemandName { get; set; }

        public string EmployeeName { get; set; }

    }
}