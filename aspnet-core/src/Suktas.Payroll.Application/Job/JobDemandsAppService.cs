using Suktas.Payroll.Master;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Suktas.Payroll.Job.Exporting;
using Suktas.Payroll.Job.Dtos;
using Suktas.Payroll.Dto;
using Abp.Application.Services.Dto;
using Suktas.Payroll.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Suktas.Payroll.Storage;

namespace Suktas.Payroll.Job
{
    [AbpAuthorize(AppPermissions.Pages_JobDemands)]
    public class JobDemandsAppService : PayrollAppServiceBase, IJobDemandsAppService
    {
        private readonly IRepository<JobDemand, Guid> _jobDemandRepository;
        private readonly IJobDemandsExcelExporter _jobDemandsExcelExporter;
        private readonly IRepository<Company, int> _lookup_companyRepository;
        private readonly IRepository<JobSkill, Guid> _lookup_jobSkillRepository;

        public JobDemandsAppService(IRepository<JobDemand, Guid> jobDemandRepository, IJobDemandsExcelExporter jobDemandsExcelExporter, IRepository<Company, int> lookup_companyRepository, IRepository<JobSkill, Guid> lookup_jobSkillRepository)
        {
            _jobDemandRepository = jobDemandRepository;
            _jobDemandsExcelExporter = jobDemandsExcelExporter;
            _lookup_companyRepository = lookup_companyRepository;
            _lookup_jobSkillRepository = lookup_jobSkillRepository;

        }

        public virtual async Task<PagedResultDto<GetJobDemandForViewDto>> GetAll(GetAllJobDemandsInput input)
        {

            var filteredJobDemands = _jobDemandRepository.GetAll()
                        .Include(e => e.CompanyFk)
                        .Include(e => e.JobSkillFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Address.Contains(input.Filter) || e.Salary.Contains(input.Filter) || e.JobSpecification.Contains(input.Filter) || e.Description.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompanyNameFilter), e => e.CompanyFk != null && e.CompanyFk.Name == input.CompanyNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.JobSkillNameFilter), e => e.JobSkillFk != null && e.JobSkillFk.Name == input.JobSkillNameFilter);

            var pagedAndFilteredJobDemands = filteredJobDemands
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var jobDemands = from o in pagedAndFilteredJobDemands
                             join o1 in _lookup_companyRepository.GetAll() on o.CompanyId equals o1.Id into j1
                             from s1 in j1.DefaultIfEmpty()

                             join o2 in _lookup_jobSkillRepository.GetAll() on o.JobSkillId equals o2.Id into j2
                             from s2 in j2.DefaultIfEmpty()

                             select new
                             {

                                 o.Name,
                                 o.Address,
                                 o.Date,
                                 o.Salary,
                                 o.ExpiredDate,
                                 Id = o.Id,
                                 CompanyName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                                 JobSkillName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                             };

            var totalCount = await filteredJobDemands.CountAsync();

            var dbList = await jobDemands.ToListAsync();
            var results = new List<GetJobDemandForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetJobDemandForViewDto()
                {
                    JobDemand = new JobDemandDto
                    {

                        Name = o.Name,
                        Address = o.Address,
                        Date = o.Date,
                        Salary = o.Salary,
                        ExpiredDate = o.ExpiredDate,
                        Id = o.Id,
                    },
                    CompanyName = o.CompanyName,
                    JobSkillName = o.JobSkillName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetJobDemandForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetJobDemandForViewDto> GetJobDemandForView(Guid id)
        {
            var jobDemand = await _jobDemandRepository.GetAsync(id);

            var output = new GetJobDemandForViewDto { JobDemand = ObjectMapper.Map<JobDemandDto>(jobDemand) };

            if (output.JobDemand.CompanyId != null)
            {
                var _lookupCompany = await _lookup_companyRepository.FirstOrDefaultAsync((int)output.JobDemand.CompanyId);
                output.CompanyName = _lookupCompany?.Name?.ToString();
            }

            if (output.JobDemand.JobSkillId != null)
            {
                var _lookupJobSkill = await _lookup_jobSkillRepository.FirstOrDefaultAsync((Guid)output.JobDemand.JobSkillId);
                output.JobSkillName = _lookupJobSkill?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_JobDemands_Edit)]
        public virtual async Task<GetJobDemandForEditOutput> GetJobDemandForEdit(EntityDto<Guid> input)
        {
            var jobDemand = await _jobDemandRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetJobDemandForEditOutput { JobDemand = ObjectMapper.Map<CreateOrEditJobDemandDto>(jobDemand) };

            if (output.JobDemand.CompanyId != null)
            {
                var _lookupCompany = await _lookup_companyRepository.FirstOrDefaultAsync((int)output.JobDemand.CompanyId);
                output.CompanyName = _lookupCompany?.Name?.ToString();
            }

            if (output.JobDemand.JobSkillId != null)
            {
                var _lookupJobSkill = await _lookup_jobSkillRepository.FirstOrDefaultAsync((Guid)output.JobDemand.JobSkillId);
                output.JobSkillName = _lookupJobSkill?.Name?.ToString();
            }

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditJobDemandDto input)
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

        [AbpAuthorize(AppPermissions.Pages_JobDemands_Create)]
        protected virtual async Task Create(CreateOrEditJobDemandDto input)
        {
            var jobDemand = ObjectMapper.Map<JobDemand>(input);

            if (AbpSession.TenantId != null)
            {
                jobDemand.TenantId = (int?)AbpSession.TenantId;
            }

            await _jobDemandRepository.InsertAsync(jobDemand);

        }

        [AbpAuthorize(AppPermissions.Pages_JobDemands_Edit)]
        protected virtual async Task Update(CreateOrEditJobDemandDto input)
        {
            var jobDemand = await _jobDemandRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, jobDemand);

        }

        [AbpAuthorize(AppPermissions.Pages_JobDemands_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _jobDemandRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetJobDemandsToExcel(GetAllJobDemandsForExcelInput input)
        {

            var filteredJobDemands = _jobDemandRepository.GetAll()
                        .Include(e => e.CompanyFk)
                        .Include(e => e.JobSkillFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Address.Contains(input.Filter) || e.Salary.Contains(input.Filter) || e.JobSpecification.Contains(input.Filter) || e.Description.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompanyNameFilter), e => e.CompanyFk != null && e.CompanyFk.Name == input.CompanyNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.JobSkillNameFilter), e => e.JobSkillFk != null && e.JobSkillFk.Name == input.JobSkillNameFilter);

            var query = (from o in filteredJobDemands
                         join o1 in _lookup_companyRepository.GetAll() on o.CompanyId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_jobSkillRepository.GetAll() on o.JobSkillId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         select new GetJobDemandForViewDto()
                         {
                             JobDemand = new JobDemandDto
                             {
                                 Name = o.Name,
                                 Address = o.Address,
                                 Date = o.Date,
                                 Salary = o.Salary,
                                 ExpiredDate = o.ExpiredDate,
                                 Id = o.Id
                             },
                             CompanyName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                             JobSkillName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                         });

            var jobDemandListDtos = await query.ToListAsync();

            return _jobDemandsExcelExporter.ExportToFile(jobDemandListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_JobDemands)]
        public async Task<List<JobDemandCompanyLookupTableDto>> GetAllCompanyForTableDropdown()
        {
            return await _lookup_companyRepository.GetAll()
                .Select(company => new JobDemandCompanyLookupTableDto
                {
                    Id = company.Id,
                    DisplayName = company == null || company.Name == null ? "" : company.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_JobDemands)]
        public async Task<List<JobDemandJobSkillLookupTableDto>> GetAllJobSkillForTableDropdown()
        {
            return await _lookup_jobSkillRepository.GetAll()
                .Select(jobSkill => new JobDemandJobSkillLookupTableDto
                {
                    Id = jobSkill.Id.ToString(),
                    DisplayName = jobSkill == null || jobSkill.Name == null ? "" : jobSkill.Name.ToString()
                }).ToListAsync();
        }

    }
}