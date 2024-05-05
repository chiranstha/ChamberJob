using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Suktas.Payroll.Master.Dtos
{
    public class GetCompanyForEditOutput
    {
        public CreateOrEditCompanyDto Company { get; set; }

        public string CompanyCategoryName { get; set; }

        public string CompanyTypeName { get; set; }

        public string LogoFileName { get; set; }

    }
}