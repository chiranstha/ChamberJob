using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Suktas.Payroll.Job.Dtos
{
    public class GetEmploymentForEditOutput
    {
        public CreateOrEditEmploymentDto Employment { get; set; }

        public string CompanyName { get; set; }

    }
}