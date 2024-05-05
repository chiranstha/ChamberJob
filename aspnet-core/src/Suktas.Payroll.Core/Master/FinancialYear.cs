using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Suktas.Payroll.Master
{
    [Table("tbl_FinancialYear")]
    public class FinancialYear : Entity<Guid>, IMustHaveTenant
    {
        public int TenantId { get; set; }

        [Required]
        public virtual string Name { get; set; }

        public virtual string FromMiti { get; set; }

        public virtual string ToMiti { get; set; }

        public virtual bool IsOldYear { get; set; }

        public virtual bool IsActive { get; set; }

    }
}