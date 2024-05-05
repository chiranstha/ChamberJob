using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Suktas.Payroll.Authorization;
using Suktas.Payroll.Job.Dtos;
using Suktas.Payroll.Master;
using Suktas.Payroll.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Suktas.Payroll.Job
{
    [AbpAuthorize(AppPermissions.Pages_JobApply)]
    public class JobApplyAppService : PayrollAppServiceBase, IJobApplyAppService
    {
        private readonly IRepository<JobApply, Guid> _jobApplyRepository;
        private readonly IRepository<Company, int> _lookup_companyRepository;
        private readonly IRepository<JobDemand, Guid> _lookup_jobDemandRepository;
        private readonly IRepository<Employee, Guid> _lookup_employeeRepository;

        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;

        public JobApplyAppService(IRepository<JobApply, Guid> jobApplyRepository, IRepository<Company, int> lookup_companyRepository, IRepository<JobDemand, Guid> lookup_jobDemandRepository, IRepository<Employee, Guid> lookup_employeeRepository, ITempFileCacheManager tempFileCacheManager, IBinaryObjectManager binaryObjectManager)
        {
            _jobApplyRepository = jobApplyRepository;
            _lookup_companyRepository = lookup_companyRepository;
            _lookup_jobDemandRepository = lookup_jobDemandRepository;
            _lookup_employeeRepository = lookup_employeeRepository;

            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;

        }

        public virtual async Task<PagedResultDto<GetJobApplyForViewDto>> GetAll(GetAllJobApplyInput input)
        {

            var filteredJobApply = _jobApplyRepository.GetAll()
                        .Include(e => e.CompanyFk)
                        .Include(e => e.JobDemandFk)
                        .Include(e => e.EmployeeFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Remark.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompanyNameFilter), e => e.CompanyFk != null && e.CompanyFk.Name == input.CompanyNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.JobDemandNameFilter), e => e.JobDemandFk != null && e.JobDemandFk.Name == input.JobDemandNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmployeeNameFilter), e => e.EmployeeFk != null && e.EmployeeFk.Name == input.EmployeeNameFilter);

            var pagedAndFilteredJobApply = filteredJobApply
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var jobApply = from o in pagedAndFilteredJobApply
                           join o1 in _lookup_companyRepository.GetAll() on o.CompanyId equals o1.Id into j1
                           from s1 in j1.DefaultIfEmpty()

                           join o2 in _lookup_jobDemandRepository.GetAll() on o.JobDemandId equals o2.Id into j2
                           from s2 in j2.DefaultIfEmpty()

                           join o3 in _lookup_employeeRepository.GetAll() on o.EmployeeId equals o3.Id into j3
                           from s3 in j3.DefaultIfEmpty()

                           select new
                           {

                               o.Date,
                               Id = o.Id,
                               CompanyName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                               JobDemandName = s2 == null || s2.Name == null ? "" : s2.Name.ToString(),
                               EmployeeName = s3 == null || s3.Name == null ? "" : s3.Name.ToString()
                           };

            var totalCount = await filteredJobApply.CountAsync();

            var dbList = await jobApply.ToListAsync();
            var results = new List<GetJobApplyForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetJobApplyForViewDto()
                {
                    Date = o.Date,
                    Id = o.Id,
                    CompanyName = o.CompanyName,
                    JobDemandName = o.JobDemandName,
                    EmployeeName = o.EmployeeName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetJobApplyForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetJobApplyForViewDto> GetJobApplyForView(Guid id)
        {
            var jobApply = await _jobApplyRepository.GetAsync(id);

            var output = new GetJobApplyForViewDto 
            {
                Date = jobApply.Date,
                CompanyId = jobApply.CompanyId,
                JobDemandId = jobApply.JobDemandId,
                EmployeeId = jobApply.EmployeeId
            };

            if (output.CompanyId != null)
            {
                var _lookupCompany = await _lookup_companyRepository.FirstOrDefaultAsync((int)output.CompanyId);
                output.CompanyName = _lookupCompany?.Name?.ToString();
            }

            if (output.JobDemandId != null)
            {
                var _lookupJobDemand = await _lookup_jobDemandRepository.FirstOrDefaultAsync((Guid)output.JobDemandId);
                output.JobDemandName = _lookupJobDemand?.Name?.ToString();
            }

            if (output.EmployeeId != null)
            {
                var _lookupEmployee = await _lookup_employeeRepository.FirstOrDefaultAsync((Guid)output.EmployeeId);
                output.EmployeeName = _lookupEmployee?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_JobApply_Edit)]
        public virtual async Task<GetJobApplyForEditOutput> GetJobApplyForEdit(EntityDto<Guid> input)
        {
            var jobApply = await _jobApplyRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetJobApplyForEditOutput 
            {
                Date = jobApply.Date,
                Document = jobApply.Document,
                Remark = jobApply.Remark,
                CompanyId = jobApply.CompanyId,
                JobDemandId = jobApply.JobDemandId,
                EmployeeId = jobApply.EmployeeId,
            };

            if (output.CompanyId != null)
            {
                var _lookupCompany = await _lookup_companyRepository.FirstOrDefaultAsync((int)output.CompanyId);
                output.CompanyName = _lookupCompany?.Name?.ToString();
            }

            if (output.JobDemandId != null)
            {
                var _lookupJobDemand = await _lookup_jobDemandRepository.FirstOrDefaultAsync((Guid)output.JobDemandId);
                output.JobDemandName = _lookupJobDemand?.Name?.ToString();
            }

            if (output.EmployeeId != null)
            {
                var _lookupEmployee = await _lookup_employeeRepository.FirstOrDefaultAsync((Guid)output.EmployeeId);
                output.EmployeeName = _lookupEmployee?.Name?.ToString();
            }

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditJobApplyDto input)
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

        [AbpAuthorize(AppPermissions.Pages_JobApply_Create)]
        protected virtual async Task Create(CreateOrEditJobApplyDto input)
        {
            var jobApply = new JobApply
            {
                Date = input.Date,
                Document = input.Document,
                Remark = input.Remark,
                CompanyId = input.CompanyId,
                JobDemandId = input.JobDemandId,
                EmployeeId = input.EmployeeId,
            };

            if (AbpSession.TenantId != null)
            {
                jobApply.TenantId = (int?)AbpSession.TenantId;
            }

            await _jobApplyRepository.InsertAsync(jobApply);
            jobApply.Document = await GetBinaryObjectFromCache(input.DocumentToken);

        }

        [AbpAuthorize(AppPermissions.Pages_JobApply_Edit)]
        protected virtual async Task Update(CreateOrEditJobApplyDto input)
        {
            var jobApply = await _jobApplyRepository.FirstOrDefaultAsync((Guid)input.Id);
            if(jobApply != null)
            {
                jobApply.Date = input.Date;
                jobApply.Document = input.Document;
                jobApply.Remark = input.Remark;
                jobApply.CompanyId = input.CompanyId;
                jobApply.JobDemandId = input.JobDemandId;
                jobApply.EmployeeId = input.EmployeeId;
                await _jobApplyRepository.UpdateAsync(jobApply);
            }
            jobApply.Document = await GetBinaryObjectFromCache(input.DocumentToken);

        }

        [AbpAuthorize(AppPermissions.Pages_JobApply_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _jobApplyRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_JobApply)]
        public async Task<List<JobApplyCompanyLookupTableDto>> GetAllCompanyForTableDropdown()
        {
            return await _lookup_companyRepository.GetAll()
                .Select(company => new JobApplyCompanyLookupTableDto
                {
                    Id = company.Id,
                    DisplayName = company == null || company.Name == null ? "" : company.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_JobApply)]
        public async Task<List<JobApplyJobDemandLookupTableDto>> GetAllJobDemandForTableDropdown()
        {
            return await _lookup_jobDemandRepository.GetAll()
                .Select(jobDemand => new JobApplyJobDemandLookupTableDto
                {
                    Id = jobDemand.Id.ToString(),
                    DisplayName = jobDemand == null || jobDemand.Name == null ? "" : jobDemand.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_JobApply)]
        public async Task<List<JobApplyEmployeeLookupTableDto>> GetAllEmployeeForTableDropdown()
        {
            return await _lookup_employeeRepository.GetAll()
                .Select(employee => new JobApplyEmployeeLookupTableDto
                {
                    Id = employee.Id.ToString(),
                    DisplayName = employee == null || employee.Name == null ? "" : employee.Name.ToString()
                }).ToListAsync();
        }

        protected virtual async Task<Guid?> GetBinaryObjectFromCache(string fileToken)
        {
            if (fileToken.IsNullOrWhiteSpace())
            {
                return null;
            }

            var fileCache = _tempFileCacheManager.GetFileInfo(fileToken);

            if (fileCache == null)
            {
                throw new UserFriendlyException("There is no such file with the token: " + fileToken);
            }

            var storedFile = new BinaryObject(AbpSession.TenantId, fileCache.File, fileCache.FileName);
            await _binaryObjectManager.SaveAsync(storedFile);

            return storedFile.Id;
        }

        protected virtual async Task<string> GetBinaryFileName(Guid? fileId)
        {
            if (!fileId.HasValue)
            {
                return null;
            }

            var file = await _binaryObjectManager.GetOrNullAsync(fileId.Value);
            return file?.Description;
        }

        [AbpAuthorize(AppPermissions.Pages_JobApply_Edit)]
        public virtual async Task RemoveDocumentFile(EntityDto<Guid> input)
        {
            var jobApply = await _jobApplyRepository.FirstOrDefaultAsync(input.Id);
            if (jobApply == null)
            {
                throw new UserFriendlyException(L("EntityNotFound"));
            }

            if (!jobApply.Document.HasValue)
            {
                throw new UserFriendlyException(L("FileNotFound"));
            }

            await _binaryObjectManager.DeleteAsync(jobApply.Document.Value);
            jobApply.Document = null;
        }

    }
}