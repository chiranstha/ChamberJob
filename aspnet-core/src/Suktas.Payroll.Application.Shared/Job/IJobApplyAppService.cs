using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Suktas.Payroll.Job.Dtos;
using Suktas.Payroll.Dto;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Suktas.Payroll.Job
{
    public interface IJobApplyAppService : IApplicationService
    {
        Task<PagedResultDto<GetJobApplyForViewDto>> GetAll(GetAllJobApplyInput input);

        Task<GetJobApplyForViewDto> GetJobApplyForView(Guid id);

        Task<GetJobApplyForEditOutput> GetJobApplyForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditJobApplyDto input);

        Task Delete(EntityDto<Guid> input);

        Task<List<JobApplyCompanyLookupTableDto>> GetAllCompanyForTableDropdown();

        Task<List<JobApplyJobDemandLookupTableDto>> GetAllJobDemandForTableDropdown();

        Task<List<JobApplyEmployeeLookupTableDto>> GetAllEmployeeForTableDropdown();

        Task RemoveDocumentFile(EntityDto<Guid> input);

    }
}