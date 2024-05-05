using Abp.Application.Services.Dto;
using System;

namespace Suktas.Payroll.Master.Dtos
{
    public class GetAllCompanyTypeInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

    }
}