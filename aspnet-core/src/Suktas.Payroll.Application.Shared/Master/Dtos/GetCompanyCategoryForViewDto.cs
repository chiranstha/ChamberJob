using Abp.Application.Services.Dto;

namespace Suktas.Payroll.Master.Dtos
{
    public class GetCompanyCategoryForViewDto : EntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }


    }
}