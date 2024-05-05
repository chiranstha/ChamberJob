using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Suktas.Payroll.Job.Dtos;
using Suktas.Payroll.Dto;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Suktas.Payroll.Job
{
    public interface IJobDemandsAppService : IApplicationService
    {
        Task<PagedResultDto<GetJobDemandForViewDto>> GetAll(GetAllJobDemandsInput input);

        Task<GetJobDemandForViewDto> GetJobDemandForView(Guid id);

        Task<GetJobDemandForEditOutput> GetJobDemandForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditJobDemandDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetJobDemandsToExcel(GetAllJobDemandsForExcelInput input);

        Task<List<JobDemandCompanyLookupTableDto>> GetAllCompanyForTableDropdown();

        Task<List<JobDemandJobSkillLookupTableDto>> GetAllJobSkillForTableDropdown();

    }
}