using Suktas.Payroll.Master;
using Suktas.Payroll.Master;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Suktas.Payroll.Master
{
    [Table("tbl_Company")]
    public class Company : Entity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Address { get; set; }

        public virtual string AuthorizedPerson { get; set; }

        [Required]
        public virtual string ContactNo { get; set; }

        public virtual BusinessNatureEnum BusinessNature { get; set; }

        public virtual string EstablishedYear { get; set; }

        public virtual string Website { get; set; }

        public virtual string VatNo { get; set; }
        //File

        public virtual Guid? Logo { get; set; } //File, (BinaryObjectId)

        public virtual int CompanyCategoryId { get; set; }

        [ForeignKey("CompanyCategoryId")]
        public CompanyCategory CompanyCategoryFk { get; set; }

        public virtual Guid CompanyTypeId { get; set; }

        [ForeignKey("CompanyTypeId")]
        public CompanyType CompanyTypeFk { get; set; }

    }
}