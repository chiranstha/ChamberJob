using Suktas.Payroll.Master;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Suktas.Payroll.Job.Dtos
{
    public class CreateOrEditEmployeeDto : EntityDto<Guid?>
    {

        public string Name { get; set; }

        public string PhoneNo { get; set; }

        public GenderEnum Gender { get; set; }

        public DateTime Dbo { get; set; }

        public string Experience { get; set; }

        public string Qualification { get; set; }

        public decimal ExpectedSalary { get; set; }

        public int CommitmentYear { get; set; }

        public Guid? Photo { get; set; }

        public string PhotoToken { get; set; }

        public Guid JobSkillId { get; set; }

    }
}