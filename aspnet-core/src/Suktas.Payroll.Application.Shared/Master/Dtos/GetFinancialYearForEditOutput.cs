using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Suktas.Payroll.Master.Dtos
{
    public class GetFinancialYearForEditOutput : EntityDto<Guid?>
    {

        [Required]
        public string Name { get; set; }

        public string FromMiti { get; set; }

        public string ToMiti { get; set; }

        public bool IsOldYear { get; set; }

    }
}