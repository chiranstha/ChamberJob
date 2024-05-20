using Suktas.Payroll.Master;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Suktas.Payroll.Job.Dtos
{
    public class CreateOrEditJobDemandDto : EntityDto<Guid?>
    {

        public string Name { get; set; }

        public string Address { get; set; }

        public string DateMiti { get; set; }

        public string Salary { get; set; }

        public string InterviewDateMiti { get; set; }

        public ExperienceLevelEnum ExperienceLevel { get; set; }

        public string ExpiredDateMiti { get; set; }

        public string JobSpecification { get; set; }

        public string Description { get; set; }

        public int CompanyId { get; set; }

        public Guid JobSkillId { get; set; }

    }
}