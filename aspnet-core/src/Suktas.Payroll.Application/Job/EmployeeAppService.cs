﻿using Suktas.Payroll.Master;

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
    [AbpAuthorize(AppPermissions.Pages_Employee)]
    public class EmployeeAppService : PayrollAppServiceBase, IEmployeeAppService
    {
        private readonly IRepository<Employee, Guid> _employeeRepository;
        private readonly IEmployeeExcelExporter _employeeExcelExporter;
        private readonly IRepository<JobSkill, Guid> _lookup_jobSkillRepository;

        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;

        public EmployeeAppService(IRepository<Employee, Guid> employeeRepository, IEmployeeExcelExporter employeeExcelExporter, IRepository<JobSkill, Guid> lookup_jobSkillRepository, ITempFileCacheManager tempFileCacheManager, IBinaryObjectManager binaryObjectManager)
        {
            _employeeRepository = employeeRepository;
            _employeeExcelExporter = employeeExcelExporter;
            _lookup_jobSkillRepository = lookup_jobSkillRepository;

            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;

        }

        public virtual async Task<PagedResultDto<GetEmployeeForViewDto>> GetAll(GetAllEmployeeInput input)
        {

            var filteredEmployee = _employeeRepository.GetAll()
                        .Include(e => e.JobSkillFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.PhoneNo.Contains(input.Filter) || e.Experience.Contains(input.Filter) || e.Qualification.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.JobSkillNameFilter), e => e.JobSkillFk != null && e.JobSkillFk.Name == input.JobSkillNameFilter);

            var pagedAndFilteredEmployee = filteredEmployee
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var employee = from o in pagedAndFilteredEmployee
                           join o1 in _lookup_jobSkillRepository.GetAll() on o.JobSkillId equals o1.Id into j1
                           from s1 in j1.DefaultIfEmpty()

                           select new
                           {

                               o.Name,
                               o.PhoneNo,
                               o.Gender,
                               o.Dbo,
                               o.Qualification,
                               o.ExpectedSalary,
                               Id = o.Id,
                               JobSkillName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                           };

            var totalCount = await filteredEmployee.CountAsync();

            var dbList = await employee.ToListAsync();
            var results = new List<GetEmployeeForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetEmployeeForViewDto()
                {
                    Employee = new EmployeeDto
                    {

                        Name = o.Name,
                        PhoneNo = o.PhoneNo,
                        Gender = o.Gender,
                        Dbo = o.Dbo,
                        Qualification = o.Qualification,
                        ExpectedSalary = o.ExpectedSalary,
                        Id = o.Id,
                    },
                    JobSkillName = o.JobSkillName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetEmployeeForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetEmployeeForViewDto> GetEmployeeForView(Guid id)
        {
            var employee = await _employeeRepository.GetAsync(id);

            var output = new GetEmployeeForViewDto { Employee = ObjectMapper.Map<EmployeeDto>(employee) };

            if (output.Employee.JobSkillId != null)
            {
                var _lookupJobSkill = await _lookup_jobSkillRepository.FirstOrDefaultAsync((Guid)output.Employee.JobSkillId);
                output.JobSkillName = _lookupJobSkill?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Employee_Edit)]
        public virtual async Task<GetEmployeeForEditOutput> GetEmployeeForEdit(EntityDto<Guid> input)
        {
            var employee = await _employeeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEmployeeForEditOutput { Employee = ObjectMapper.Map<CreateOrEditEmployeeDto>(employee) };

            if (output.Employee.JobSkillId != null)
            {
                var _lookupJobSkill = await _lookup_jobSkillRepository.FirstOrDefaultAsync((Guid)output.Employee.JobSkillId);
                output.JobSkillName = _lookupJobSkill?.Name?.ToString();
            }

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditEmployeeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Employee_Create)]
        protected virtual async Task Create(CreateOrEditEmployeeDto input)
        {
            var employee = ObjectMapper.Map<Employee>(input);

            if (AbpSession.TenantId != null)
            {
                employee.TenantId = (int?)AbpSession.TenantId;
            }

            await _employeeRepository.InsertAsync(employee);
            employee.Photo = await GetBinaryObjectFromCache(input.PhotoToken);

        }

        [AbpAuthorize(AppPermissions.Pages_Employee_Edit)]
        protected virtual async Task Update(CreateOrEditEmployeeDto input)
        {
            var employee = await _employeeRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, employee);
            employee.Photo = await GetBinaryObjectFromCache(input.PhotoToken);

        }

        [AbpAuthorize(AppPermissions.Pages_Employee_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _employeeRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetEmployeeToExcel(GetAllEmployeeForExcelInput input)
        {

            var filteredEmployee = _employeeRepository.GetAll()
                        .Include(e => e.JobSkillFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.PhoneNo.Contains(input.Filter) || e.Experience.Contains(input.Filter) || e.Qualification.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.JobSkillNameFilter), e => e.JobSkillFk != null && e.JobSkillFk.Name == input.JobSkillNameFilter);

            var query = (from o in filteredEmployee
                         join o1 in _lookup_jobSkillRepository.GetAll() on o.JobSkillId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetEmployeeForViewDto()
                         {
                             Employee = new EmployeeDto
                             {
                                 Name = o.Name,
                                 PhoneNo = o.PhoneNo,
                                 Gender = o.Gender,
                                 Dbo = o.Dbo,
                                 Qualification = o.Qualification,
                                 ExpectedSalary = o.ExpectedSalary,
                                 Id = o.Id
                             },
                             JobSkillName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                         });

            var employeeListDtos = await query.ToListAsync();

            return _employeeExcelExporter.ExportToFile(employeeListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Employee)]
        public async Task<List<EmployeeJobSkillLookupTableDto>> GetAllJobSkillForTableDropdown()
        {
            return await _lookup_jobSkillRepository.GetAll()
                .Select(jobSkill => new EmployeeJobSkillLookupTableDto
                {
                    Id = jobSkill.Id.ToString(),
                    DisplayName = jobSkill == null || jobSkill.Name == null ? "" : jobSkill.Name.ToString()
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

        [AbpAuthorize(AppPermissions.Pages_Employee_Edit)]
        public virtual async Task RemovePhotoFile(EntityDto<Guid> input)
        {
            var employee = await _employeeRepository.FirstOrDefaultAsync(input.Id);
            if (employee == null)
            {
                throw new UserFriendlyException(L("EntityNotFound"));
            }

            if (!employee.Photo.HasValue)
            {
                throw new UserFriendlyException(L("FileNotFound"));
            }

            await _binaryObjectManager.DeleteAsync(employee.Photo.Value);
            employee.Photo = null;
        }

    }
}