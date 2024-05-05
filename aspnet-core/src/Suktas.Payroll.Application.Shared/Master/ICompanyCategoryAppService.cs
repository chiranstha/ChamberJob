using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Suktas.Payroll.Master.Dtos;
using Suktas.Payroll.Dto;

namespace Suktas.Payroll.Master
{
    public interface ICompanyCategoryAppService : IApplicationService
    {
        Task<PagedResultDto<GetCompanyCategoryForViewDto>> GetAll(GetAllCompanyCategoryInput input);

        Task<GetCompanyCategoryForViewDto> GetCompanyCategoryForView(int id);

        Task<GetCompanyCategoryForEditOutput> GetCompanyCategoryForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditCompanyCategoryDto input);

        Task Delete(EntityDto input);

    }
}