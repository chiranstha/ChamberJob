using Abp.Application.Services.Dto;
using Suktas.Payroll.Master;
using System;

namespace Suktas.Payroll.Job.Dtos
{
    public class GetEmployeeForViewDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string PhoneNo { get; set; }

        public GenderEnum Gender { get; set; }

        public DateTime Dbo { get; set; }

        public string Qualification { get; set; }

        public decimal ExpectedSalary { get; set; }

        public Guid JobSkillId { get; set; }

        public string JobSkillName { get; set; }

    }
}