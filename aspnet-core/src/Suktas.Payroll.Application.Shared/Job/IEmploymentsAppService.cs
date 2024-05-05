using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Suktas.Payroll.Dto;
using System.Collections.Generic;
using Suktas.Payroll.Job.Dtos;

namespace Suktas.Payroll.Job
{
    public interface IEmploymentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetEmploymentForViewDto>> GetAll(GetAllEmploymentsInput input);

        Task<GetEmploymentForViewDto> GetEmploymentForView(Guid id);

        Task<GetEmploymentForEditOutput> GetEmploymentForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditEmploymentDto input);

        Task Delete(EntityDto<Guid> input);

        Task<List<EmploymentCompanyLookupTableDto>> GetAllCompanyForTableDropdown();

    }
}