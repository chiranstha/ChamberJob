using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Suktas.Payroll.Master.Dtos
{
    public class GetCompanyCategoryForEditOutput
    {
        public CreateOrEditCompanyCategoryDto CompanyCategory { get; set; }

    }
}