using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Suktas.Payroll.Master.Dtos;
using Suktas.Payroll.Dto;

namespace Suktas.Payroll.Master
{
    public interface IJobSkillAppService : IApplicationService
    {
        Task<PagedResultDto<GetJobSkillForViewDto>> GetAll(GetAllJobSkillInput input);

        Task<GetJobSkillForViewDto> GetJobSkillForView(Guid id);

        Task<GetJobSkillForEditOutput> GetJobSkillForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditJobSkillDto input);

        Task Delete(EntityDto<Guid> input);

    }
}