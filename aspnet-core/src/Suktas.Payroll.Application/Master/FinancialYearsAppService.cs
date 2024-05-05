using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Suktas.Payroll.Authorization;
using Suktas.Payroll.Master.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Suktas.Payroll.Master
{
    [AbpAuthorize(AppPermissions.Pages_FinancialYears)]
    public class FinancialYearsAppService : PayrollAppServiceBase, IFinancialYearsAppService
    {
        private readonly IRepository<FinancialYear, Guid> _financialYearRepository;

        public FinancialYearsAppService(IRepository<FinancialYear, Guid> financialYearRepository)
        {
            _financialYearRepository = financialYearRepository;

        }

        public virtual async Task<PagedResultDto<GetFinancialYearForViewDto>> GetAll(GetAllFinancialYearsInput input)
        {

            var filteredFinancialYears = _financialYearRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.FromMiti.Contains(input.Filter) || e.ToMiti.Contains(input.Filter));

            var pagedAndFilteredFinancialYears = filteredFinancialYears
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var financialYears = from o in pagedAndFilteredFinancialYears
                                 select new
                                 {

                                     o.Name,
                                     o.FromMiti,
                                     o.ToMiti,
                                     Id = o.Id
                                 };

            var totalCount = await filteredFinancialYears.CountAsync();

            var dbList = await financialYears.ToListAsync();
            var results = new List<GetFinancialYearForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetFinancialYearForViewDto()
                {

                    Name = o.Name,
                    FromMiti = o.FromMiti,
                    ToMiti = o.ToMiti,
                    Id = o.Id,
                };

                results.Add(res);
            }

            return new PagedResultDto<GetFinancialYearForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetFinancialYearForViewDto> GetFinancialYearForView(Guid id)
        {
            var financialYear = await _financialYearRepository.GetAsync(id);

            var output = new GetFinancialYearForViewDto
            { 
                Id = financialYear.Id,
                Name = financialYear.Name,
                FromMiti = financialYear.FromMiti,
                ToMiti = financialYear.ToMiti,
            };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_FinancialYears_Edit)]
        public virtual async Task<GetFinancialYearForEditOutput> GetFinancialYearForEdit(EntityDto<Guid> input)
        {
            var financialYear = await _financialYearRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetFinancialYearForEditOutput 
            {
                Name = financialYear.Name,
                FromMiti = financialYear.FromMiti,
                ToMiti = financialYear.ToMiti,
                IsOldYear = financialYear.IsOldYear,
                Id = input.Id
            };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditFinancialYearDto input)
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

        [AbpAuthorize(AppPermissions.Pages_FinancialYears_Create)]
        protected virtual async Task Create(CreateOrEditFinancialYearDto input)
        {
            var financialYear = new FinancialYear
            {
                Name = input.Name,
                FromMiti = input.FromMiti,
                ToMiti = input.ToMiti,
                IsOldYear = input.IsOldYear,
            };

            if (AbpSession.TenantId != null)
            {
                financialYear.TenantId = (int)AbpSession.TenantId;
            }

            await _financialYearRepository.InsertAsync(financialYear);

        }

        [AbpAuthorize(AppPermissions.Pages_FinancialYears_Edit)]
        protected virtual async Task Update(CreateOrEditFinancialYearDto input)
        {
            var financialYear = await _financialYearRepository.FirstOrDefaultAsync(e => e.Id == input.Id);
            if(financialYear != null)
            {
                financialYear.Name = input.Name;
                financialYear.FromMiti = input.FromMiti;
                financialYear.ToMiti = input.ToMiti;
                financialYear.IsOldYear = input.IsOldYear;
                await _financialYearRepository.UpdateAsync(financialYear);
            }

        }

        [AbpAuthorize(AppPermissions.Pages_FinancialYears_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _financialYearRepository.DeleteAsync(input.Id);
        }

    }
}