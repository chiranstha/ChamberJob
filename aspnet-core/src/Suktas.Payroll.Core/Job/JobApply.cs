using Suktas.Payroll.Master;
using Suktas.Payroll.Job;
using Suktas.Payroll.Job;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Suktas.Payroll.Job
{
    [Table("tbl_JobApply")]
    public class JobApply : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public virtual DateTime Date { get; set; }
        //File

        public virtual Guid? Document { get; set; } //File, (BinaryObjectId)

        public virtual string Remark { get; set; }

        public virtual int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company CompanyFk { get; set; }

        public virtual Guid JobDemandId { get; set; }

        [ForeignKey("JobDemandId")]
        public JobDemand JobDemandFk { get; set; }

        public virtual Guid EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee EmployeeFk { get; set; }

    }
}