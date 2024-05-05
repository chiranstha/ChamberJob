using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Suktas.Payroll.Job.Dtos
{
    public class GetJobApplyForEditOutput
    {
        public CreateOrEditJobApplyDto JobApply { get; set; }

        public string CompanyName { get; set; }

        public string JobDemandName { get; set; }

        public string EmployeeName { get; set; }

        public string DocumentFileName { get; set; }

    }
}