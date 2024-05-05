using Suktas.Payroll.Master;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Suktas.Payroll.Job
{
    [Table("Employments")]
    public class Employment : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public virtual int Total { get; set; }

        public virtual int Male { get; set; }

        public virtual int Female { get; set; }

        public virtual int Foreign { get; set; }

        public virtual int Impairment { get; set; }

        public virtual decimal SalaryStart { get; set; }

        public virtual decimal SalaryEnd { get; set; }

        public virtual int AgeStart { get; set; }

        public virtual int AgeEnd { get; set; }

        public virtual int Parment { get; set; }

        public virtual int Temporary { get; set; }

        public virtual int Trainer { get; set; }

        public virtual int DailyWages { get; set; }

        public virtual int? CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company CompanyFk { get; set; }

    }
}