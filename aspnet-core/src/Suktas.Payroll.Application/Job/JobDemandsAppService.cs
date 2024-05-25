using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Suktas.Payroll.Authorization;
using Suktas.Payroll.Dto;
using Suktas.Payroll.Job.Dtos;
using Suktas.Payroll.Job.Exporting;
using Suktas.Payroll.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using Abp.UI;
using Suktas.Payroll.Authorization.Users;
using Suktas.Payroll.NepaliDate;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Suktas.Payroll.Job
{
    [AbpAuthorize(AppPermissions.Pages_JobDemands)]
    public class JobDemandsAppService : PayrollAppServiceBase, IJobDemandsAppService
    {
        private readonly IRepository<JobDemand, Guid> _jobDemandRepository;
        private readonly IJobDemandsExcelExporter _jobDemandsExcelExporter;
        private readonly IRepository<Company, int> _lookupCompanyRepository;
        private readonly IRepository<JobSkill, Guid> _lookupJobSkillRepository;

        public JobDemandsAppService(IRepository<JobDemand, Guid> jobDemandRepository,
            IJobDemandsExcelExporter jobDemandsExcelExporter, IRepository<Company, int> lookupCompanyRepository,
            IRepository<JobSkill, Guid> lookupJobSkillRepository)
        {
            _jobDemandRepository = jobDemandRepository;
            _jobDemandsExcelExporter = jobDemandsExcelExporter;
            _lookupCompanyRepository = lookupCompanyRepository;
            _lookupJobSkillRepository = lookupJobSkillRepository;
        }

        public virtual async Task<PagedResultDto<GetJobDemandForViewDto>> GetAll(GetAllJobDemandsInput input)
        {
            var filteredJobDemands = _jobDemandRepository.GetAll()
                .Include(e => e.CompanyFk)
                .Include(e => e.JobSkillFk)
                .Where(e => e.UserId == AbpSession.GetUserId())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    e => false || e.Name.Contains(input.Filter) || e.Address.Contains(input.Filter) ||
                         e.Salary.Contains(input.Filter) || e.JobSpecification.Contains(input.Filter) ||
                         e.Description.Contains(input.Filter))
                .WhereIf(!string.IsNullOrWhiteSpace(input.CompanyNameFilter),
                    e => e.CompanyFk != null && e.CompanyFk.Name == input.CompanyNameFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.JobSkillNameFilter),
                    e => e.JobSkillFk != null && e.JobSkillFk.Name == input.JobSkillNameFilter);

            var pagedAndFilteredJobDemands = filteredJobDemands
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var jobDemands = from o in pagedAndFilteredJobDemands
                join o1 in _lookupCompanyRepository.GetAll() on o.CompanyId equals o1.Id into j1
                from s1 in j1.DefaultIfEmpty()
                join o2 in _lookupJobSkillRepository.GetAll() on o.JobSkillId equals o2.Id into j2
                from s2 in j2.DefaultIfEmpty()
                select new
                {
                    o.Name,
                    o.Address,
                    o.Date,
                    o.Salary,
                    o.ExpiredDate,
                    o.Id,
                    CompanyName = s1 == null || s1.Name == null ? "" : s1.Name,
                    JobSkillName = s2 == null || s2.Name == null ? "" : s2.Name
                };

            var totalCount = await filteredJobDemands.CountAsync();

            var dbList = await jobDemands.ToListAsync();
            var results = dbList.Select(o => new GetJobDemandForViewDto()
                {
                    Name = o.Name,
                    Address = o.Address,
                    Date = o.Date,
                    Salary = o.Salary,
                    ExpiredDate = o.ExpiredDate,
                    Id = o.Id,
                    CompanyName = o.CompanyName,
                    JobSkillName = o.JobSkillName
                })
                .ToList();

            return new PagedResultDto<GetJobDemandForViewDto>(
                totalCount,
                results
            );
        }


        public virtual async Task<PagedResultDto<GetJobDemandForViewDto>> GetAllDemand(GetAllJobDemandsInput input)
        {
            var filteredJobDemands = _jobDemandRepository.GetAll()
                .Include(e => e.CompanyFk)
                .Include(e => e.JobSkillFk)
                .Where(e => e.UserId == AbpSession.GetUserId())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    e => false || e.Name.Contains(input.Filter) || e.Address.Contains(input.Filter) ||
                         e.Salary.Contains(input.Filter) || e.JobSpecification.Contains(input.Filter) ||
                         e.Description.Contains(input.Filter))
                .WhereIf(!string.IsNullOrWhiteSpace(input.CompanyNameFilter),
                    e => e.CompanyFk != null && e.CompanyFk.Name == input.CompanyNameFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.JobSkillNameFilter),
                    e => e.JobSkillFk != null && e.JobSkillFk.Name == input.JobSkillNameFilter);

            var pagedAndFilteredJobDemands = filteredJobDemands
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var jobDemands = from o in pagedAndFilteredJobDemands
                join o1 in _lookupCompanyRepository.GetAll() on o.CompanyId equals o1.Id into j1
                from s1 in j1.DefaultIfEmpty()
                join o2 in _lookupJobSkillRepository.GetAll() on o.JobSkillId equals o2.Id into j2
                from s2 in j2.DefaultIfEmpty()
                select new
                {
                    o.Name,
                    o.Address,
                    o.Date,
                    o.Salary,
                    o.ExpiredDate,
                    o.Id,
                    CompanyName = s1 == null || s1.Name == null ? "" : s1.Name,
                    JobSkillName = s2 == null || s2.Name == null ? "" : s2.Name
                };

            var totalCount = await filteredJobDemands.CountAsync();

            var dbList = await jobDemands.ToListAsync();
            var results = dbList.Select(o => new GetJobDemandForViewDto()
                {
                    Name = o.Name,
                    Address = o.Address,
                    Date = o.Date,
                    Salary = o.Salary,
                    ExpiredDate = o.ExpiredDate,
                    Id = o.Id,
                    CompanyName = o.CompanyName,
                    JobSkillName = o.JobSkillName
                })
                .ToList();

            return new PagedResultDto<GetJobDemandForViewDto>(
                totalCount,
                results
            );
        }

        public virtual async Task<GetJobDemandForViewDto> GetJobDemandForView(Guid id)
        {
            var jobDemand = await _jobDemandRepository.GetAll()
                .Include(e => e.CompanyFk)
                .Include(e => e.JobSkillFk)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (jobDemand == null)
            {
                throw new UserFriendlyException(L("InvalidId"));
            }

            var output = new GetJobDemandForViewDto
            {
                Name = jobDemand.Name,
                Address = jobDemand.Address,
                Date = jobDemand.Date,
                Salary = jobDemand.Salary,
                ExpiredDate = jobDemand.ExpiredDate,
                CompanyId = jobDemand.CompanyId,
                JobSkillId = jobDemand.JobSkillId,
                CompanyName = jobDemand.CompanyFk.Name,
                JobSkillName = jobDemand.JobSkillFk.Name
            };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_JobDemands_Edit)]
        public virtual async Task<GetJobDemandForEditOutput> GetJobDemandForEdit(EntityDto<Guid> input)
        {
            var jobDemand = await _jobDemandRepository.GetAll()
                .Include(e => e.CompanyFk)
                .Include(e => e.JobSkillFk)
                .FirstOrDefaultAsync(e => e.Id == input.Id);
            if (jobDemand == null)
            {
                throw new UserFriendlyException(L("InvalidId"));
            }

            var output = new GetJobDemandForEditOutput
            {
                Name = jobDemand.Name,
                Address = jobDemand.Address,
                Date = jobDemand.Date,
                Salary = jobDemand.Salary,
                InterviewDate = jobDemand.InterviewDate,
                ExperienceLevel = jobDemand.ExperienceLevel,
                ExpiredDate = jobDemand.ExpiredDate,
                JobSpecification = jobDemand.JobSpecification,
                Description = jobDemand.Description,
                CompanyId = jobDemand.CompanyId,
                JobSkillId = jobDemand.JobSkillId,
                CompanyName = jobDemand.CompanyFk.Name,
                JobSkillName = jobDemand.JobSkillFk.Name
            };


            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditJobDemandDto input)
        {
            if (input.Id == null || input.Id == Guid.Empty)
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
            var jobDemand = new JobDemand
            {
                Name = input.Name,
                Description = input.Description,
                Address = input.Address,
                
                DateMiti = input.DateMiti,
                Salary = input.Salary,
               
                ExperienceLevel = input.ExperienceLevel,
                InterviewDate = DateConverter.ConvertToEnglish(input.InterviewDateMiti),
                Date = DateConverter.ConvertToEnglish(input.DateMiti),
                ExpiredDate = DateConverter.ConvertToEnglish(input.ExpiredDateMiti),
                JobSpecification = input.JobSpecification,
                CompanyId = input.CompanyId,
                JobSkillId = input.JobSkillId,
                TenantId = AbpSession.TenantId,
                UserId = AbpSession.GetUserId()
            };


            await _jobDemandRepository.InsertAsync(jobDemand);
        }

        [AbpAuthorize(AppPermissions.Pages_JobDemands_Edit)]
        protected virtual async Task Update(CreateOrEditJobDemandDto input)
        {
            var jobDemand = await _jobDemandRepository.FirstOrDefaultAsync(e => e.Id == input.Id);
            if (jobDemand != null)
            {
                jobDemand.Name = input.Name;
                jobDemand.Description = input.Description;
                jobDemand.Address = input.Address;
                jobDemand.Salary = input.Salary;
                jobDemand.ExperienceLevel = input.ExperienceLevel;
                jobDemand.JobSpecification = input.JobSpecification;
                jobDemand.CompanyId = input.CompanyId;
                jobDemand.JobSkillId = input.JobSkillId;
                jobDemand.InterviewDate = DateConverter.ConvertToEnglish(input.InterviewDateMiti);
                jobDemand.Date = DateConverter.ConvertToEnglish(input.DateMiti);
                jobDemand.ExpiredDate = DateConverter.ConvertToEnglish(input.ExpiredDateMiti);
                await _jobDemandRepository.UpdateAsync(jobDemand);
            }

           
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
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    e => false || e.Name.Contains(input.Filter) || e.Address.Contains(input.Filter) ||
                         e.Salary.Contains(input.Filter) || e.JobSpecification.Contains(input.Filter) ||
                         e.Description.Contains(input.Filter))
                .WhereIf(!string.IsNullOrWhiteSpace(input.CompanyNameFilter),
                    e => e.CompanyFk != null && e.CompanyFk.Name == input.CompanyNameFilter)
                .WhereIf(!string.IsNullOrWhiteSpace(input.JobSkillNameFilter),
                    e => e.JobSkillFk != null && e.JobSkillFk.Name == input.JobSkillNameFilter);

            var query = (from o in filteredJobDemands
                join o1 in _lookupCompanyRepository.GetAll() on o.CompanyId equals o1.Id into j1
                from s1 in j1.DefaultIfEmpty()
                join o2 in _lookupJobSkillRepository.GetAll() on o.JobSkillId equals o2.Id into j2
                from s2 in j2.DefaultIfEmpty()
                select new GetJobDemandForViewDto()
                {
                    Name = o.Name,
                    Address = o.Address,
                    Date = o.Date,
                    Salary = o.Salary,
                    ExpiredDate = o.ExpiredDate,
                    Id = o.Id,
                    CompanyName = s1 == null || s1.Name == null ? "" : s1.Name,
                    JobSkillName = s2 == null || s2.Name == null ? "" : s2.Name
                });

            var jobDemandListDtos = await query.ToListAsync();

            return _jobDemandsExcelExporter.ExportToFile(jobDemandListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_JobDemands)]
        public async Task<List<JobDemandCompanyLookupTableDto>> GetAllCompanyForTableDropdown()
        {
            return await _lookupCompanyRepository.GetAll().Where(e => e.UserId == AbpSession.GetUserId())
                .Select(company => new JobDemandCompanyLookupTableDto
                {
                    Id = company.Id,
                    DisplayName = company == null || company.Name == null ? "" : company.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_JobDemands)]
        public async Task<List<JobDemandJobSkillLookupTableDto>> GetAllJobSkillForTableDropdown()
        {
            return await _lookupJobSkillRepository.GetAll()
                .Select(jobSkill => new JobDemandJobSkillLookupTableDto
                {
                    Id = jobSkill.Id,
                    DisplayName = jobSkill == null || jobSkill.Name == null ? "" : jobSkill.Name.ToString()
                }).ToListAsync();
        }
    }
}