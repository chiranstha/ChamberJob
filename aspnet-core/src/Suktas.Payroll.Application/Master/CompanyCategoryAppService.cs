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
    [AbpAuthorize(AppPermissions.Pages_CompanyCategory)]
    public class CompanyCategoryAppService : PayrollAppServiceBase, ICompanyCategoryAppService
    {
        private readonly IRepository<CompanyCategory> _companyCategoryRepository;

        public CompanyCategoryAppService(IRepository<CompanyCategory> companyCategoryRepository)
        {
            _companyCategoryRepository = companyCategoryRepository;

        }

        public virtual async Task<PagedResultDto<GetCompanyCategoryForViewDto>> GetAll(GetAllCompanyCategoryInput input)
        {

            var filteredCompanyCategory = _companyCategoryRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Description.Contains(input.Filter));

            var pagedAndFilteredCompanyCategory = filteredCompanyCategory
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var companyCategory = from o in pagedAndFilteredCompanyCategory
                                  select new
                                  {

                                      o.Name,
                                      o.Description,
                                      Id = o.Id
                                  };

            var totalCount = await filteredCompanyCategory.CountAsync();

            var dbList = await companyCategory.ToListAsync();
            var results = new List<GetCompanyCategoryForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetCompanyCategoryForViewDto()
                {
                    CompanyCategory = new CompanyCategoryDto
                    {

                        Name = o.Name,
                        Description = o.Description,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetCompanyCategoryForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetCompanyCategoryForViewDto> GetCompanyCategoryForView(int id)
        {
            var companyCategory = await _companyCategoryRepository.GetAsync(id);

            var output = new GetCompanyCategoryForViewDto { CompanyCategory = ObjectMapper.Map<CompanyCategoryDto>(companyCategory) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_CompanyCategory_Edit)]
        public virtual async Task<GetCompanyCategoryForEditOutput> GetCompanyCategoryForEdit(EntityDto input)
        {
            var companyCategory = await _companyCategoryRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCompanyCategoryForEditOutput { CompanyCategory = ObjectMapper.Map<CreateOrEditCompanyCategoryDto>(companyCategory) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditCompanyCategoryDto input)
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

        [AbpAuthorize(AppPermissions.Pages_CompanyCategory_Create)]
        protected virtual async Task Create(CreateOrEditCompanyCategoryDto input)
        {
            var companyCategory = ObjectMapper.Map<CompanyCategory>(input);

            if (AbpSession.TenantId != null)
            {
                companyCategory.TenantId = (int?)AbpSession.TenantId;
            }

            await _companyCategoryRepository.InsertAsync(companyCategory);

        }

        [AbpAuthorize(AppPermissions.Pages_CompanyCategory_Edit)]
        protected virtual async Task Update(CreateOrEditCompanyCategoryDto input)
        {
            var companyCategory = await _companyCategoryRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, companyCategory);

        }

        [AbpAuthorize(AppPermissions.Pages_CompanyCategory_Delete)]
        public virtual async Task Delete(EntityDto input)
        {
            await _companyCategoryRepository.DeleteAsync(input.Id);
        }

    }
}