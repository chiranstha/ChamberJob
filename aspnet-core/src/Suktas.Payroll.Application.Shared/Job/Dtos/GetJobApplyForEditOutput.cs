using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Suktas.Payroll.Job.Dtos
{
    public class GetJobApplyForEditOutput : EntityDto<Guid?>
    {

        public DateTime Date { get; set; }

        public Guid? Document { get; set; }

        public string DocumentToken { get; set; }

        public string Remark { get; set; }

        public int CompanyId { get; set; }

        public Guid JobDemandId { get; set; }

        public Guid EmployeeId { get; set; }

        public string CompanyName { get; set; }

        public string JobDemandName { get; set; }

        public string EmployeeName { get; set; }

        public string DocumentFileName { get; set; }

    }
}