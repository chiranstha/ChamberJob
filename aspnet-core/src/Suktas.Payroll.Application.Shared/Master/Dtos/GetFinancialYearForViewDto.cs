using Abp.Application.Services.Dto;
using System;

namespace Suktas.Payroll.Master.Dtos
{
    public class GetFinancialYearForViewDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string FromMiti { get; set; }

        public string ToMiti { get; set; }

    }
}