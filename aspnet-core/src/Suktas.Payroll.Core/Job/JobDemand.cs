using Suktas.Payroll.Master;
using Suktas.Payroll.Master;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Suktas.Payroll.Authorization.Users;

namespace Suktas.Payroll.Job
{
    [Table("tbl_JobDemand")]
    public class JobDemand : Entity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public virtual string Name { get; set; }

        public virtual string Address { get; set; }

        public virtual DateTime Date { get; set; }
        public virtual string DateMiti { get; set; }

        public virtual string Salary { get; set; }

        public virtual DateTime InterviewDate { get; set; }
        public virtual DateTime InterviewDateMiti { get; set; }

        public virtual ExperienceLevelEnum ExperienceLevel { get; set; }

        public virtual DateTime ExpiredDate { get; set; }
        public virtual DateTime ExpiredDateMiti { get; set; }

        public virtual string JobSpecification { get; set; }

        public virtual string Description { get; set; }

        public virtual int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company CompanyFk { get; set; }

        public virtual Guid JobSkillId { get; set; }

        [ForeignKey("JobSkillId")]
        public JobSkill JobSkillFk { get; set; }


        public virtual long UserId { get; set; }

        [ForeignKey("UserId")]
        public User UserFk { get; set; }

    }
}