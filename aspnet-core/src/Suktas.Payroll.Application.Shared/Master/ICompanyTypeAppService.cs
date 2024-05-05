using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Suktas.Payroll.Master.Dtos;
using Suktas.Payroll.Dto;

namespace Suktas.Payroll.Master
{
    public interface ICompanyTypeAppService : IApplicationService
    {
        Task<PagedResultDto<GetCompanyTypeForViewDto>> GetAll(GetAllCompanyTypeInput input);

        Task<GetCompanyTypeForViewDto> GetCompanyTypeForView(Guid id);

        Task<GetCompanyTypeForEditOutput> GetCompanyTypeForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditCompanyTypeDto input);

        Task Delete(EntityDto<Guid> input);

    }
}