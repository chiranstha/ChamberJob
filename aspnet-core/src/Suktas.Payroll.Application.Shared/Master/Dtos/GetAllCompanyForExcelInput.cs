using Abp.Application.Services.Dto;
using System;

namespace Suktas.Payroll.Master.Dtos
{
    public class GetAllCompanyForExcelInput
    {
        public string Filter { get; set; }

        public string CompanyCategoryNameFilter { get; set; }

        public string CompanyTypeNameFilter { get; set; }

    }
}