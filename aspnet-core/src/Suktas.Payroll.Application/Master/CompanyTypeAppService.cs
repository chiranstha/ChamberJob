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
    [AbpAuthorize(AppPermissions.Pages_CompanyType)]
    public class CompanyTypeAppService : PayrollAppServiceBase, ICompanyTypeAppService
    {
        private readonly IRepository<CompanyType, Guid> _companyTypeRepository;

        public CompanyTypeAppService(IRepository<CompanyType, Guid> companyTypeRepository)
        {
            _companyTypeRepository = companyTypeRepository;

        }

        public virtual async Task<PagedResultDto<GetCompanyTypeForViewDto>> GetAll(GetAllCompanyTypeInput input)
        {

            var filteredCompanyType = _companyTypeRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Description.Contains(input.Filter));

            var pagedAndFilteredCompanyType = filteredCompanyType
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var companyType = from o in pagedAndFilteredCompanyType
                              select new
                              {
                                  o.Name,
                                  o.Description,
                                  Id = o.Id
                              };

            var totalCount = await filteredCompanyType.CountAsync();

            var dbList = await companyType.ToListAsync();
            var results = new List<GetCompanyTypeForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetCompanyTypeForViewDto()
                {
                    Name = o.Name,
                    Description = o.Description,
                    Id = o.Id,
                };

                results.Add(res);
            }

            return new PagedResultDto<GetCompanyTypeForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetCompanyTypeForViewDto> GetCompanyTypeForView(Guid id)
        {
            var companyType = await _companyTypeRepository.GetAsync(id);

            var output = new GetCompanyTypeForViewDto
            {
                Id = id,
                Name = companyType.Name,
                Description = companyType.Description,
            };
            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_CompanyType_Edit)]
        public virtual async Task<GetCompanyTypeForEditOutput> GetCompanyTypeForEdit(EntityDto<Guid> input)
        {
            var companyType = await _companyTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCompanyTypeForEditOutput
            { 
                Id = companyType.Id,
                Name = companyType.Name,
                Description = companyType.Description,
            };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditCompanyTypeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_CompanyType_Create)]
        protected virtual async Task Create(CreateOrEditCompanyTypeDto input)
        {
            var companyType = new CompanyType
            {
                Name = input.Name,
                Description = input.Description,
            };

            if (AbpSession.TenantId != null)
            {
                companyType.TenantId = (int?)AbpSession.TenantId;
            }

            await _companyTypeRepository.InsertAsync(companyType);

        }

        [AbpAuthorize(AppPermissions.Pages_CompanyType_Edit)]
        protected virtual async Task Update(CreateOrEditCompanyTypeDto input)
        {
            var companyType = await _companyTypeRepository.FirstOrDefaultAsync((Guid)input.Id);
            if(companyType != null)
            {
                companyType.Name = input.Name;
                companyType.Description = input.Description;
            }
            ObjectMapper.Map(input, companyType);

        }

        [AbpAuthorize(AppPermissions.Pages_CompanyType_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _companyTypeRepository.DeleteAsync(input.Id);
        }

    }
}