using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Suktas.Payroll.Master.Dtos;
using Suktas.Payroll.Dto;
using System.Collections.Generic;
using System.Collections.Generic;

namespace Suktas.Payroll.Master
{
    public interface ICompanyAppService : IApplicationService
    {
        Task<PagedResultDto<GetCompanyForViewDto>> GetAll(GetAllCompanyInput input);

        Task<GetCompanyForViewDto> GetCompanyForView(int id);

        Task<GetCompanyForEditOutput> GetCompanyForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditCompanyDto input);

        Task Delete(EntityDto input);

        Task<FileDto> GetCompanyToExcel(GetAllCompanyForExcelInput input);

        Task<List<CompanyCompanyCategoryLookupTableDto>> GetAllCompanyCategoryForTableDropdown();

        Task<List<CompanyCompanyTypeLookupTableDto>> GetAllCompanyTypeForTableDropdown();

        Task RemoveLogoFile(EntityDto input);

    }
}