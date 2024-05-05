using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Suktas.Payroll.Master.Dtos
{
    public class CreateOrEditQualificationDto : EntityDto<Guid?>
    {

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

    }
}