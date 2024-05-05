using System;
using Abp.Application.Services.Dto;

namespace Suktas.Payroll.Master.Dtos
{
    public class FinancialYearDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string FromMiti { get; set; }

        public string ToMiti { get; set; }

    }
}