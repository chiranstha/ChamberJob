using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Suktas.Payroll.Job.Dtos
{
    public class GetJobDemandForEditOutput
    {
        public CreateOrEditJobDemandDto JobDemand { get; set; }

        public string CompanyName { get; set; }

        public string JobSkillName { get; set; }

    }
}