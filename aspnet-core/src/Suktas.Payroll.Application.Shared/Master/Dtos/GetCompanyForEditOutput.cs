using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Suktas.Payroll.Master.Dtos
{
    public class GetCompanyForEditOutput : EntityDto<int?>
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public string AuthorizedPerson { get; set; }

        [Required]
        public string ContactNo { get; set; }

        public BusinessNatureEnum BusinessNature { get; set; }

        public string EstablishedYear { get; set; }

        public string Website { get; set; }

        public string VatNo { get; set; }

        public Guid? Logo { get; set; }

        public string LogoToken { get; set; }

        public int CompanyCategoryId { get; set; }

        public Guid CompanyTypeId { get; set; }

        public string CompanyCategoryName { get; set; }

        public string CompanyTypeName { get; set; }

        public string LogoFileName { get; set; }

    }
}