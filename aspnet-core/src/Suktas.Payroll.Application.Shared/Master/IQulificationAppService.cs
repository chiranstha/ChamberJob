using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Suktas.Payroll.Master.Dtos;
using Suktas.Payroll.Dto;

namespace Suktas.Payroll.Master
{
    public interface IQualificationAppService : IApplicationService
    {
        Task<PagedResultDto<GetQualificationForViewDto>> GetAll(GetAllQualificationInput input);

        Task<GetQualificationForViewDto> GetQualificationForView(Guid id);

        Task<GetQualificationForEditOutput> GetQualificationForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditQualificationDto input);

        Task Delete(EntityDto<Guid> input);

    }
}