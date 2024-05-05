using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Suktas.Payroll.Master.Dtos;
using Suktas.Payroll.Dto;
using Abp.Application.Services.Dto;
using Suktas.Payroll.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Suktas.Payroll.Storage;

namespace Suktas.Payroll.Master
{
    [AbpAuthorize(AppPermissions.Pages_JobSkill)]
    public class JobSkillAppService : PayrollAppServiceBase, IJobSkillAppService
    {
        private readonly IRepository<JobSkill, Guid> _jobSkillRepository;

        public JobSkillAppService(IRepository<JobSkill, Guid> jobSkillRepository)
        {
            _jobSkillRepository = jobSkillRepository;

        }

        public virtual async Task<PagedResultDto<GetJobSkillForViewDto>> GetAll(GetAllJobSkillInput input)
        {

            var filteredJobSkill = _jobSkillRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Description.Contains(input.Filter));

            var pagedAndFilteredJobSkill = filteredJobSkill
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var jobSkill = from o in pagedAndFilteredJobSkill
                           select new
                           {

                               o.Name,
                               o.Description,
                               Id = o.Id
                           };

            var totalCount = await filteredJobSkill.CountAsync();

            var dbList = await jobSkill.ToListAsync();
            var results = new List<GetJobSkillForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetJobSkillForViewDto()
                {
                    JobSkill = new JobSkillDto
                    {

                        Name = o.Name,
                        Description = o.Description,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetJobSkillForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetJobSkillForViewDto> GetJobSkillForView(Guid id)
        {
            var jobSkill = await _jobSkillRepository.GetAsync(id);

            var output = new GetJobSkillForViewDto { JobSkill = ObjectMapper.Map<JobSkillDto>(jobSkill) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_JobSkill_Edit)]
        public virtual async Task<GetJobSkillForEditOutput> GetJobSkillForEdit(EntityDto<Guid> input)
        {
            var jobSkill = await _jobSkillRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetJobSkillForEditOutput { JobSkill = ObjectMapper.Map<CreateOrEditJobSkillDto>(jobSkill) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditJobSkillDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_JobSkill_Create)]
        protected virtual async Task Create(CreateOrEditJobSkillDto input)
        {
            var jobSkill = ObjectMapper.Map<JobSkill>(input);

            if (AbpSession.TenantId != null)
            {
                jobSkill.TenantId = (int?)AbpSession.TenantId;
            }

            await _jobSkillRepository.InsertAsync(jobSkill);

        }

        [AbpAuthorize(AppPermissions.Pages_JobSkill_Edit)]
        protected virtual async Task Update(CreateOrEditJobSkillDto input)
        {
            var jobSkill = await _jobSkillRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, jobSkill);

        }

        [AbpAuthorize(AppPermissions.Pages_JobSkill_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _jobSkillRepository.DeleteAsync(input.Id);
        }

    }
}