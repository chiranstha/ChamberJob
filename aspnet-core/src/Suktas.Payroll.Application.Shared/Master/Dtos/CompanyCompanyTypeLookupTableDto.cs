using System;
using Abp.Application.Services.Dto;

namespace Suktas.Payroll.Master.Dtos
{
    public class CompanyCompanyTypeLookupTableDto
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }
    }
}