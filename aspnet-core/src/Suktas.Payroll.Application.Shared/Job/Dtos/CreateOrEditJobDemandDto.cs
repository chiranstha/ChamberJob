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

        public DateTime Date { get; set; }

        public string Salary { get; set; }

        public DateTime InterviewDate { get; set; }

        public ExperienceLevelEnum ExperienceLevel { get; set; }

        public DateTime ExpiredDate { get; set; }

        public string JobSpecification { get; set; }

        public string Description { get; set; }

        public int CompanyId { get; set; }

        public Guid JobSkillId { get; set; }

    }
}