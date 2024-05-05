using Suktas.Payroll.Master;

using System;
using Abp.Application.Services.Dto;

namespace Suktas.Payroll.Job.Dtos
{
    public class EmployeeDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string PhoneNo { get; set; }

        public GenderEnum Gender { get; set; }

        public DateTime Dbo { get; set; }

        public string Qualification { get; set; }

        public decimal ExpectedSalary { get; set; }

        public Guid JobSkillId { get; set; }

    }
}