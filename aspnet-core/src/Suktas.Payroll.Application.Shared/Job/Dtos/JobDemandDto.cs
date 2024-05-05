using Suktas.Payroll.Master;

using System;
using Abp.Application.Services.Dto;

namespace Suktas.Payroll.Job.Dtos
{
    public class JobDemandDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public string Salary { get; set; }

        public DateTime ExpiredDate { get; set; }

        public int CompanyId { get; set; }

        public Guid JobSkillId { get; set; }

    }
}