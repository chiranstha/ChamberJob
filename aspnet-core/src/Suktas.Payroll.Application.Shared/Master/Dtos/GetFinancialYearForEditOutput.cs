using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Suktas.Payroll.Master.Dtos
{
    public class GetFinancialYearForEditOutput
    {
        public CreateOrEditFinancialYearDto FinancialYear { get; set; }

    }
}