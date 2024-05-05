using Suktas.Payroll.Master;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Suktas.Payroll.Job
{
    [Table("tbl_Employee")]
    public class Employee : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public virtual string Name { get; set; }

        public virtual string PhoneNo { get; set; }

        public virtual GenderEnum Gender { get; set; }

        public virtual DateTime Dbo { get; set; }

        public virtual string Experience { get; set; }

        public virtual string Qualification { get; set; }

        public virtual decimal ExpectedSalary { get; set; }

        public virtual int CommitmentYear { get; set; }
        //File

        public virtual Guid? Photo { get; set; } //File, (BinaryObjectId)

        public virtual Guid JobSkillId { get; set; }

        [ForeignKey("JobSkillId")]
        public JobSkill JobSkillFk { get; set; }

    }
}