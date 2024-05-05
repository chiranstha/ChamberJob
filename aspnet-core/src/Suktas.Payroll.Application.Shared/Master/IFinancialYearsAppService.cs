using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Suktas.Payroll.Master.Dtos;
using Suktas.Payroll.Dto;

namespace Suktas.Payroll.Master
{
    public interface IFinancialYearsAppService : IApplicationService
    {
        Task<PagedResultDto<GetFinancialYearForViewDto>> GetAll(GetAllFinancialYearsInput input);

        Task<GetFinancialYearForViewDto> GetFinancialYearForView(Guid id);

        Task<GetFinancialYearForEditOutput> GetFinancialYearForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditFinancialYearDto input);

        Task Delete(EntityDto<Guid> input);

    }
}