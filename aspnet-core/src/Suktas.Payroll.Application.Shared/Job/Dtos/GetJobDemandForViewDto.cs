using Abp.Application.Services.Dto;
using System;

namespace Suktas.Payroll.Job.Dtos
{
    public class GetJobDemandForViewDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public string Salary { get; set; }

        public DateTime ExpiredDate { get; set; }

        public int CompanyId { get; set; }

        public Guid JobSkillId { get; set; }

        public string CompanyName { get; set; }

        public string JobSkillName { get; set; }

    }
}