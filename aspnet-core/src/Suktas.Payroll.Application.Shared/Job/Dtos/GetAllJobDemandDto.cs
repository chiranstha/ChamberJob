using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace Suktas.Payroll.Job.Dtos
{
    public class GetAllJobDemandReportDto:EntityDto<Guid>
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
        public int RequiredQty { get; set; }
    }
}
