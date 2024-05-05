using Suktas.Payroll.Master;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Suktas.Payroll.Master.Exporting;
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
    [AbpAuthorize(AppPermissions.Pages_Company)]
    public class CompanyAppService : PayrollAppServiceBase, ICompanyAppService
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly ICompanyExcelExporter _companyExcelExporter;
        private readonly IRepository<CompanyCategory, int> _lookup_companyCategoryRepository;
        private readonly IRepository<CompanyType, Guid> _lookup_companyTypeRepository;

        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;

        public CompanyAppService(IRepository<Company> companyRepository, ICompanyExcelExporter companyExcelExporter, IRepository<CompanyCategory, int> lookup_companyCategoryRepository, IRepository<CompanyType, Guid> lookup_companyTypeRepository, ITempFileCacheManager tempFileCacheManager, IBinaryObjectManager binaryObjectManager)
        {
            _companyRepository = companyRepository;
            _companyExcelExporter = companyExcelExporter;
            _lookup_companyCategoryRepository = lookup_companyCategoryRepository;
            _lookup_companyTypeRepository = lookup_companyTypeRepository;

            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;

        }

        public virtual async Task<PagedResultDto<GetCompanyForViewDto>> GetAll(GetAllCompanyInput input)
        {

            var filteredCompany = _companyRepository.GetAll()
                        .Include(e => e.CompanyCategoryFk)
                        .Include(e => e.CompanyTypeFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Address.Contains(input.Filter) || e.AuthorizedPerson.Contains(input.Filter) || e.ContactNo.Contains(input.Filter) || e.EstablishedYear.Contains(input.Filter) || e.Website.Contains(input.Filter) || e.VatNo.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompanyCategoryNameFilter), e => e.CompanyCategoryFk != null && e.CompanyCategoryFk.Name == input.CompanyCategoryNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompanyTypeNameFilter), e => e.CompanyTypeFk != null && e.CompanyTypeFk.Name == input.CompanyTypeNameFilter);

            var pagedAndFilteredCompany = filteredCompany
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var company = from o in pagedAndFilteredCompany
                          join o1 in _lookup_companyCategoryRepository.GetAll() on o.CompanyCategoryId equals o1.Id into j1
                          from s1 in j1.DefaultIfEmpty()

                          join o2 in _lookup_companyTypeRepository.GetAll() on o.CompanyTypeId equals o2.Id into j2
                          from s2 in j2.DefaultIfEmpty()

                          select new
                          {

                              o.Name,
                              o.Address,
                              o.AuthorizedPerson,
                              o.ContactNo,
                              o.BusinessNature,
                              o.EstablishedYear,
                              Id = o.Id,
                              CompanyCategoryName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                              CompanyTypeName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                          };

            var totalCount = await filteredCompany.CountAsync();

            var dbList = await company.ToListAsync();
            var results = new List<GetCompanyForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetCompanyForViewDto()
                {
                    Company = new CompanyDto
                    {

                        Name = o.Name,
                        Address = o.Address,
                        AuthorizedPerson = o.AuthorizedPerson,
                        ContactNo = o.ContactNo,
                        BusinessNature = o.BusinessNature,
                        EstablishedYear = o.EstablishedYear,
                        Id = o.Id,
                    },
                    CompanyCategoryName = o.CompanyCategoryName,
                    CompanyTypeName = o.CompanyTypeName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetCompanyForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetCompanyForViewDto> GetCompanyForView(int id)
        {
            var company = await _companyRepository.GetAsync(id);

            var output = new GetCompanyForViewDto { Company = ObjectMapper.Map<CompanyDto>(company) };

            if (output.Company.CompanyCategoryId != null)
            {
                var _lookupCompanyCategory = await _lookup_companyCategoryRepository.FirstOrDefaultAsync((int)output.Company.CompanyCategoryId);
                output.CompanyCategoryName = _lookupCompanyCategory?.Name?.ToString();
            }

            if (output.Company.CompanyTypeId != null)
            {
                var _lookupCompanyType = await _lookup_companyTypeRepository.FirstOrDefaultAsync((Guid)output.Company.CompanyTypeId);
                output.CompanyTypeName = _lookupCompanyType?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Company_Edit)]
        public virtual async Task<GetCompanyForEditOutput> GetCompanyForEdit(EntityDto input)
        {
            var company = await _companyRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCompanyForEditOutput { Company = ObjectMapper.Map<CreateOrEditCompanyDto>(company) };

            if (output.Company.CompanyCategoryId != null)
            {
                var _lookupCompanyCategory = await _lookup_companyCategoryRepository.FirstOrDefaultAsync((int)output.Company.CompanyCategoryId);
                output.CompanyCategoryName = _lookupCompanyCategory?.Name?.ToString();
            }

            if (output.Company.CompanyTypeId != null)
            {
                var _lookupCompanyType = await _lookup_companyTypeRepository.FirstOrDefaultAsync((Guid)output.Company.CompanyTypeId);
                output.CompanyTypeName = _lookupCompanyType?.Name?.ToString();
            }

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditCompanyDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Company_Create)]
        protected virtual async Task Create(CreateOrEditCompanyDto input)
        {
            var company = ObjectMapper.Map<Company>(input);

            if (AbpSession.TenantId != null)
            {
                company.TenantId = (int?)AbpSession.TenantId;
            }

            await _companyRepository.InsertAsync(company);
            company.Logo = await GetBinaryObjectFromCache(input.LogoToken);

        }

        [AbpAuthorize(AppPermissions.Pages_Company_Edit)]
        protected virtual async Task Update(CreateOrEditCompanyDto input)
        {
            var company = await _companyRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, company);
            company.Logo = await GetBinaryObjectFromCache(input.LogoToken);

        }

        [AbpAuthorize(AppPermissions.Pages_Company_Delete)]
        public virtual async Task Delete(EntityDto input)
        {
            await _companyRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetCompanyToExcel(GetAllCompanyForExcelInput input)
        {

            var filteredCompany = _companyRepository.GetAll()
                        .Include(e => e.CompanyCategoryFk)
                        .Include(e => e.CompanyTypeFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Address.Contains(input.Filter) || e.AuthorizedPerson.Contains(input.Filter) || e.ContactNo.Contains(input.Filter) || e.EstablishedYear.Contains(input.Filter) || e.Website.Contains(input.Filter) || e.VatNo.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompanyCategoryNameFilter), e => e.CompanyCategoryFk != null && e.CompanyCategoryFk.Name == input.CompanyCategoryNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompanyTypeNameFilter), e => e.CompanyTypeFk != null && e.CompanyTypeFk.Name == input.CompanyTypeNameFilter);

            var query = (from o in filteredCompany
                         join o1 in _lookup_companyCategoryRepository.GetAll() on o.CompanyCategoryId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_companyTypeRepository.GetAll() on o.CompanyTypeId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         select new GetCompanyForViewDto()
                         {
                             Company = new CompanyDto
                             {
                                 Name = o.Name,
                                 Address = o.Address,
                                 AuthorizedPerson = o.AuthorizedPerson,
                                 ContactNo = o.ContactNo,
                                 BusinessNature = o.BusinessNature,
                                 EstablishedYear = o.EstablishedYear,
                                 Id = o.Id
                             },
                             CompanyCategoryName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                             CompanyTypeName = s2 == null || s2.Name == null ? "" : s2.Name.ToString()
                         });

            var companyListDtos = await query.ToListAsync();

            return _companyExcelExporter.ExportToFile(companyListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Company)]
        public async Task<List<CompanyCompanyCategoryLookupTableDto>> GetAllCompanyCategoryForTableDropdown()
        {
            return await _lookup_companyCategoryRepository.GetAll()
                .Select(companyCategory => new CompanyCompanyCategoryLookupTableDto
                {
                    Id = companyCategory.Id,
                    DisplayName = companyCategory == null || companyCategory.Name == null ? "" : companyCategory.Name.ToString()
                }).ToListAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Company)]
        public async Task<List<CompanyCompanyTypeLookupTableDto>> GetAllCompanyTypeForTableDropdown()
        {
            return await _lookup_companyTypeRepository.GetAll()
                .Select(companyType => new CompanyCompanyTypeLookupTableDto
                {
                    Id = companyType.Id.ToString(),
                    DisplayName = companyType == null || companyType.Name == null ? "" : companyType.Name.ToString()
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

        [AbpAuthorize(AppPermissions.Pages_Company_Edit)]
        public virtual async Task RemoveLogoFile(EntityDto input)
        {
            var company = await _companyRepository.FirstOrDefaultAsync(input.Id);
            if (company == null)
            {
                throw new UserFriendlyException(L("EntityNotFound"));
            }

            if (!company.Logo.HasValue)
            {
                throw new UserFriendlyException(L("FileNotFound"));
            }

            await _binaryObjectManager.DeleteAsync(company.Logo.Value);
            company.Logo = null;
        }

    }
}