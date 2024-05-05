using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Suktas.Payroll.Job.Dtos
{
    public class GetEmployeeForEditOutput
    {
        public CreateOrEditEmployeeDto Employee { get; set; }

        public string JobSkillName { get; set; }

        public string PhotoFileName { get; set; }

    }
}