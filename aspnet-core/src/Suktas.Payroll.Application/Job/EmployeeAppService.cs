using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Suktas.Payroll.Authorization;
using Suktas.Payroll.Dto;
using Suktas.Payroll.Job.Dtos;
using Suktas.Payroll.Job.Exporting;
using Suktas.Payroll.Master;
using Suktas.Payroll.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Suktas.Payroll.Job
{
    [AbpAuthorize]
    public class EmployeeAppService : PayrollAppServiceBase, IEmployeeAppService
    {
        private readonly IRepository<Employee, Guid> _employeeRepository;
        private readonly IEmployeeExcelExporter _employeeExcelExporter;
        private readonly IRepository<JobSkill, Guid> _lookupJobSkillRepository;

        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;

        public EmployeeAppService(IRepository<Employee, Guid> employeeRepository, IEmployeeExcelExporter employeeExcelExporter, IRepository<JobSkill, Guid> lookupJobSkillRepository, ITempFileCacheManager tempFileCacheManager, IBinaryObjectManager binaryObjectManager)
        {
            _employeeRepository = employeeRepository;
            _employeeExcelExporter = employeeExcelExporter;
            _lookupJobSkillRepository = lookupJobSkillRepository;

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
                           join o1 in _lookupJobSkillRepository.GetAll() on o.JobSkillId equals o1.Id into j1
                           from s1 in j1.DefaultIfEmpty()

                           select new
                           {
                               o.Name,
                               o.PhoneNo,
                               o.Gender,
                               o.Dbo,
                               o.Qualification,
                               o.ExpectedSalary,
                               o.Id,
                               JobSkillName = s1 == null || s1.Name == null ? "" : s1.Name
                           };

            var totalCount = await filteredEmployee.CountAsync();

            var dbList = await employee.ToListAsync();
            var results = new List<GetEmployeeForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetEmployeeForViewDto()
                {
                    Name = o.Name,
                    PhoneNo = o.PhoneNo,
                    Gender = o.Gender,
                    Dbo = o.Dbo,
                    Qualification = o.Qualification,
                    ExpectedSalary = o.ExpectedSalary,
                    Id = o.Id,
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

            var output = new GetEmployeeForViewDto
            {
                Name = employee.Name,
                PhoneNo = employee.PhoneNo,
                Gender = employee.Gender,
                Dbo = employee.Dbo,
                Qualification = employee.Qualification,
                ExpectedSalary = employee.ExpectedSalary,
                Id = employee.Id,
                JobSkillId = employee.JobSkillId
            };

            if (output.JobSkillId != null)
            {
                var lookupJobSkill = await _lookupJobSkillRepository.FirstOrDefaultAsync((Guid)output.JobSkillId);
                output.JobSkillName = lookupJobSkill?.Name;
            }

            return output;
        }

        [AbpAuthorize]
        public virtual async Task<GetEmployeeForEditOutput> GetEmployeeForEdit(EntityDto<Guid> input)
        {
            var employee = await _employeeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEmployeeForEditOutput
            {
                Name = employee.Name,
                PhoneNo = employee.PhoneNo,
                Gender = employee.Gender,
                Dbo = employee.Dbo,
                Qualification = employee.Qualification,
                Experience = employee.Experience,
                ExpectedSalary = employee.ExpectedSalary,
                CommitmentYear = employee.CommitmentYear,
                Photo = employee.Photo,
                JobSkillId = employee.JobSkillId
            };

            if (output.JobSkillId != null)
            {
                var lookupJobSkill = await _lookupJobSkillRepository.FirstOrDefaultAsync((Guid)output.JobSkillId);
                output.JobSkillName = lookupJobSkill?.Name;
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

        [AbpAuthorize]
        protected virtual async Task Create(CreateOrEditEmployeeDto input)
        {
            var employee = new Employee
            {
                Name = input.Name,
                PhoneNo = input.PhoneNo,
                Gender = input.Gender,
                Dbo = input.Dbo,
                Experience = input.Experience,
                Qualification = input.Qualification,
                ExpectedSalary = input.ExpectedSalary,
                CommitmentYear = input.CommitmentYear,
                Photo = input.Photo,
                JobSkillId = input.JobSkillId
            };

            if (AbpSession.TenantId != null)
            {
                employee.TenantId = (int?)AbpSession.TenantId;
            }

            await _employeeRepository.InsertAsync(employee);
            employee.Photo = await GetBinaryObjectFromCache(input.PhotoToken);

        }

        [AbpAuthorize]
        protected virtual async Task Update(CreateOrEditEmployeeDto input)
        {
            var employee = await _employeeRepository.FirstOrDefaultAsync(e=>e.Id==input.Id);
            if (employee != null)
            {
                employee.Name = input.Name;
                employee.PhoneNo = input.PhoneNo;
                employee.Gender = input.Gender;
                employee.Dbo = input.Dbo;
                employee.Experience = input.Experience;
                employee.Qualification = input.Qualification;
                employee.ExpectedSalary = input.ExpectedSalary;
                employee.CommitmentYear = input.CommitmentYear;
                employee.Photo = input.Photo;
                employee.JobSkillId = input.JobSkillId;
                await _employeeRepository.UpdateAsync(employee);
            }

            if (employee != null) employee.Photo = await GetBinaryObjectFromCache(input.PhotoToken);
        }

        [AbpAuthorize]
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
                         join o1 in _lookupJobSkillRepository.GetAll() on o.JobSkillId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetEmployeeForViewDto()
                         {
                             Name = o.Name,
                             PhoneNo = o.PhoneNo,
                             Gender = o.Gender,
                             Dbo = o.Dbo,
                             Qualification = o.Qualification,
                             ExpectedSalary = o.ExpectedSalary,
                             Id = o.Id,
                             JobSkillName = s1 == null || s1.Name == null ? "" : s1.Name
                         });

            var employeeListDtos = await query.ToListAsync();

            return _employeeExcelExporter.ExportToFile(employeeListDtos);
        }

        [AbpAuthorize]
        public async Task<List<EmployeeJobSkillLookupTableDto>> GetAllJobSkillForTableDropdown()
        {
            return await _lookupJobSkillRepository.GetAll()
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

        [AbpAuthorize]
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