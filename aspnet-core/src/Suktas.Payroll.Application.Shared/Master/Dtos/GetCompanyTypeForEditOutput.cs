using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Suktas.Payroll.Master.Dtos
{
    public class GetCompanyTypeForEditOutput
    {
        public CreateOrEditCompanyTypeDto CompanyType { get; set; }

    }
}