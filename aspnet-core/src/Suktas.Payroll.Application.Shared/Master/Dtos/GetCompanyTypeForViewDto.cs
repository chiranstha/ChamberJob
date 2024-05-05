using Abp.Application.Services.Dto;
using System;

namespace Suktas.Payroll.Master.Dtos
{
    public class GetCompanyTypeForViewDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

    }
}