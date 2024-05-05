using Suktas.Payroll.Master;

using System;
using Abp.Application.Services.Dto;

namespace Suktas.Payroll.Master.Dtos
{
    public class CompanyDto : EntityDto
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string AuthorizedPerson { get; set; }

        public string ContactNo { get; set; }

        public BusinessNatureEnum BusinessNature { get; set; }

        public string EstablishedYear { get; set; }

        public int CompanyCategoryId { get; set; }

        public Guid CompanyTypeId { get; set; }

    }
}