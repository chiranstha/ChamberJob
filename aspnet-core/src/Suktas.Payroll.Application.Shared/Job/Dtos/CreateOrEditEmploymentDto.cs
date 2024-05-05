using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Suktas.Payroll.Job.Dtos
{
    public class CreateOrEditEmploymentDto : EntityDto<Guid?>
    {

        public int Total { get; set; }

        public int Male { get; set; }

        public int Female { get; set; }

        public int Foreign { get; set; }

        public int Impairment { get; set; }

        public decimal SalaryStart { get; set; }

        public decimal SalaryEnd { get; set; }

        public int AgeStart { get; set; }

        public int AgeEnd { get; set; }

        public int Parment { get; set; }

        public int Temporary { get; set; }

        public int Trainer { get; set; }

        public int DailyWages { get; set; }

        public int? CompanyId { get; set; }

    }
}