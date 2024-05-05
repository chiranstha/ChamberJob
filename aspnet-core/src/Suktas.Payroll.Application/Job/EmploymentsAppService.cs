using Suktas.Payroll.Master;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Suktas.Payroll.Dto;
using Abp.Application.Services.Dto;
using Suktas.Payroll.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Suktas.Payroll.Storage;
using Suktas.Payroll.Job.Dtos;

namespace Suktas.Payroll.Job
{
    [AbpAuthorize(AppPermissions.Pages_Employments)]
    public class EmploymentsAppService : PayrollAppServiceBase, IEmploymentsAppService
    {
        private readonly IRepository<Employment, Guid> _employmentRepository;
        private readonly IRepository<Company, int> _lookup_companyRepository;

        public EmploymentsAppService(IRepository<Employment, Guid> employmentRepository, IRepository<Company, int> lookup_companyRepository)
        {
            _employmentRepository = employmentRepository;
            _lookup_companyRepository = lookup_companyRepository;

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
                              join o1 in _lookup_companyRepository.GetAll() on o.CompanyId equals o1.Id into j1
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
                                  CompanyName = s1 == null || s1.Name == null ? "" : s1.Name.ToString()
                              };

            var totalCount = await filteredEmployments.CountAsync();

            var dbList = await employments.ToListAsync();
            var results = new List<GetEmploymentForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetEmploymentForViewDto()
                {
                    Employment = new EmploymentDto
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
                    },
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

            var output = new GetEmploymentForViewDto { Employment = ObjectMapper.Map<EmploymentDto>(employment) };

            if (output.Employment.CompanyId != null)
            {
                var _lookupCompany = await _lookup_companyRepository.FirstOrDefaultAsync((int)output.Employment.CompanyId);
                output.CompanyName = _lookupCompany?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Employments_Edit)]
        public virtual async Task<GetEmploymentForEditOutput> GetEmploymentForEdit(EntityDto<Guid> input)
        {
            var employment = await _employmentRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEmploymentForEditOutput { Employment = ObjectMapper.Map<CreateOrEditEmploymentDto>(employment) };

            if (output.Employment.CompanyId != null)
            {
                var _lookupCompany = await _lookup_companyRepository.FirstOrDefaultAsync((int)output.Employment.CompanyId);
                output.CompanyName = _lookupCompany?.Name?.ToString();
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
            var employment = ObjectMapper.Map<Employment>(input);

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
            return await _lookup_companyRepository.GetAll()
                .Select(company => new EmploymentCompanyLookupTableDto
                {
                    Id = company.Id,
                    DisplayName = company == null || company.Name == null ? "" : company.Name.ToString()
                }).ToListAsync();
        }

    }
}