using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Suktas.Payroll.Authorization;
using Suktas.Payroll.Job.Dtos;
using Suktas.Payroll.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Suktas.Payroll.Job
{
    [AbpAuthorize(AppPermissions.Pages_Employments)]
    public class EmploymentsAppService : PayrollAppServiceBase, IEmploymentsAppService
    {
        private readonly IRepository<Employment, Guid> _employmentRepository;
        private readonly IRepository<Company, int> _lookupCompanyRepository;

        public EmploymentsAppService(IRepository<Employment, Guid> employmentRepository, IRepository<Company, int> lookupCompanyRepository)
        {
            _employmentRepository = employmentRepository;
            _lookupCompanyRepository = lookupCompanyRepository;

        }

        public virtual async Task<PagedResultDto<GetEmploymentForViewDto>> GetAll(GetAllEmploymentsInput input)
        {

            var filteredEmployments = _employmentRepository.GetAll()
                        .Include(e => e.CompanyFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.MinMaleFilter != null, e => e.Male >= input.MinMaleFilter)
                        .WhereIf(input.MaxMaleFilter != null, e => e.Male <= input.MaxMaleFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompanyNameFilter), e => e.CompanyFk != null && e.CompanyFk.Name == input.CompanyNameFilter);

            var pagedAndFilteredEmployments = filteredEmployments
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var employments = from o in pagedAndFilteredEmployments
                              join o1 in _lookupCompanyRepository.GetAll() on o.CompanyId equals o1.Id into j1
                              from s1 in j1.DefaultIfEmpty()

                              select new
                              {

                                  o.Total,
                                  o.Male,
                                  o.Female,
                                  o.Foreign,
                                  o.Impairment,
                                  o.SalaryStart,
                                  o.SalaryEnd,
                                  o.AgeStart,
                                  o.AgeEnd,
                                  o.Parment,
                                  o.Temporary,
                                  o.Trainer,
                                  o.DailyWages,
                                  o.Id,
                                  CompanyName = s1 == null || s1.Name == null ? "" : s1.Name
                              };

            var totalCount = await filteredEmployments.CountAsync();

            var dbList = await employments.ToListAsync();
            var results = new List<GetEmploymentForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetEmploymentForViewDto()
                {

                    Total = o.Total,
                    Male = o.Male,
                    Female = o.Female,
                    Foreign = o.Foreign,
                    Impairment = o.Impairment,
                    SalaryStart = o.SalaryStart,
                    SalaryEnd = o.SalaryEnd,
                    AgeStart = o.AgeStart,
                    AgeEnd = o.AgeEnd,
                    Parment = o.Parment,
                    Temporary = o.Temporary,
                    Trainer = o.Trainer,
                    DailyWages = o.DailyWages,
                    Id = o.Id,
                    CompanyName = o.CompanyName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetEmploymentForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetEmploymentForViewDto> GetEmploymentForView(Guid id)
        {
            var employment = await _employmentRepository.GetAsync(id);

            var output = new GetEmploymentForViewDto
            {
                Total = employment.Total,
                Male = employment.Male,
                Female = employment.Female,
                Foreign = employment.Foreign,
                Impairment = employment.Impairment,
                SalaryEnd = employment.SalaryEnd,
                SalaryStart = employment.AgeStart,
                AgeStart = employment.AgeStart,
                AgeEnd = employment.AgeEnd,
                Parment = employment.Parment,
                Temporary = employment.Temporary,
                Trainer = employment.Trainer,
                DailyWages = employment.DailyWages,
                CompanyId = employment.CompanyId,
            };

            if (output.CompanyId != null)
            {
                var lookupCompany = await _lookupCompanyRepository.FirstOrDefaultAsync((int)output.CompanyId);
                output.CompanyName = lookupCompany?.Name;
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Employments_Edit)]
        public virtual async Task<GetEmploymentForEditOutput> GetEmploymentForEdit(EntityDto<Guid> input)
        {
            var employment = await _employmentRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEmploymentForEditOutput
            {
                Total = employment.Total,
                Male = employment.Male,
                Female = employment.Female,
                Foreign = employment.Foreign,
                Impairment = employment.Impairment,
                SalaryEnd = employment.SalaryEnd,
                SalaryStart = employment.SalaryStart,
                AgeEnd = employment.AgeEnd,
                AgeStart = employment.AgeStart,
                Parment = employment.Parment,
                Temporary = employment.Temporary,
                Trainer = employment.Trainer,
                DailyWages = employment.DailyWages,
                CompanyId = employment.CompanyId
            };

            if (output.CompanyId != null)
            {
                var lookupCompany = await _lookupCompanyRepository.FirstOrDefaultAsync((int)output.CompanyId);
                output.CompanyName = lookupCompany?.Name;
            }

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditEmploymentDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Employments_Create)]
        protected virtual async Task Create(CreateOrEditEmploymentDto input)
        {
            var employment = new Employment
            {
                Total = input.Total,
                Male = input.Male,
                Female = input.Female,
                Foreign = input.Foreign,
                Impairment = input.Impairment,
                SalaryEnd = input.SalaryEnd,
                SalaryStart = input.SalaryStart,
                AgeEnd = input.AgeEnd,
                AgeStart = input.AgeStart,
                Parment = input.Parment,
                Temporary = input.Temporary,
                Trainer = input.Trainer,
                DailyWages = input.DailyWages,
                CompanyId = input.CompanyId,
            };

            if (AbpSession.TenantId != null)
            {
                employment.TenantId = AbpSession.TenantId;
            }

            await _employmentRepository.InsertAsync(employment);

        }

        [AbpAuthorize(AppPermissions.Pages_Employments_Edit)]
        protected virtual async Task Update(CreateOrEditEmploymentDto input)
        {
            var employment = await _employmentRepository.FirstOrDefaultAsync((Guid)input.Id);
            if(employment != null)
            {
                employment.Total = input.Total;
                employment.Male = input.Male;
                employment.Female = input.Female;
                employment.Foreign = input.Foreign;
                employment.Impairment = input.Impairment;
                employment.SalaryEnd = input.SalaryEnd;
                employment.SalaryStart = input.SalaryStart;
                employment.AgeEnd = input.AgeEnd;
                employment.AgeStart = input.AgeStart;
                employment.Parment = input.Parment;
                employment.Temporary = input.Temporary;
                employment.Trainer = input.Trainer;
                employment.DailyWages = input.DailyWages;
                employment.CompanyId = input.CompanyId;
                await _employmentRepository.UpdateAsync(employment);
            }
            ObjectMapper.Map(input, employment);

        }

        [AbpAuthorize(AppPermissions.Pages_Employments_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _employmentRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_Employments)]
        public async Task<List<EmploymentCompanyLookupTableDto>> GetAllCompanyForTableDropdown()
        {
            return await _lookupCompanyRepository.GetAll()
                .Select(company => new EmploymentCompanyLookupTableDto
                {
                    Id = company.Id,
                    DisplayName = company == null || company.Name == null ? "" : company.Name.ToString()
                }).ToListAsync();
        }

    }
}