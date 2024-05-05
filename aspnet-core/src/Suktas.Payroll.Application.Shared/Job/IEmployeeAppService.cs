using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Suktas.Payroll.Job.Dtos;
using Suktas.Payroll.Dto;
using System.Collections.Generic;

namespace Suktas.Payroll.Job
{
    public interface IEmployeeAppService : IApplicationService
    {
        Task<PagedResultDto<GetEmployeeForViewDto>> GetAll(GetAllEmployeeInput input);

        Task<GetEmployeeForViewDto> GetEmployeeForView(Guid id);

        Task<GetEmployeeForEditOutput> GetEmployeeForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditEmployeeDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetEmployeeToExcel(GetAllEmployeeForExcelInput input);

        Task<List<EmployeeJobSkillLookupTableDto>> GetAllJobSkillForTableDropdown();

        Task RemovePhotoFile(EntityDto<Guid> input);

    }
}