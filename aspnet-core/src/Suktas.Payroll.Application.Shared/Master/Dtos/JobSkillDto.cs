using System;
using Abp.Application.Services.Dto;

namespace Suktas.Payroll.Master.Dtos
{
    public class JobSkillDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

    }
}