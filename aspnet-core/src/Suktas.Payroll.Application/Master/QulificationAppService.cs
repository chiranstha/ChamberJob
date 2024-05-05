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
    [AbpAuthorize(AppPermissions.Pages_Qualification)]
    public class QualificationAppService : PayrollAppServiceBase, IQualificationAppService
    {
        private readonly IRepository<Qualification, Guid> _qualificationRepository;

        public QualificationAppService(IRepository<Qualification, Guid> qualificationRepository)
        {
            _qualificationRepository = qualificationRepository;

        }

        public virtual async Task<PagedResultDto<GetQualificationForViewDto>> GetAll(GetAllQualificationInput input)
        {

            var filteredQualification = _qualificationRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Description.Contains(input.Filter));

            var pagedAndFilteredQualification = filteredQualification
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var qualification = from o in pagedAndFilteredQualification
                               select new
                               {

                                   o.Name,
                                   o.Description,
                                   Id = o.Id
                               };

            var totalCount = await filteredQualification.CountAsync();

            var dbList = await qualification.ToListAsync();
            var results = new List<GetQualificationForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetQualificationForViewDto()
                {
                    Qualification = new QualificationDto
                    {

                        Name = o.Name,
                        Description = o.Description,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetQualificationForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetQualificationForViewDto> GetQualificationForView(Guid id)
        {
            var qualification = await _qualificationRepository.GetAsync(id);

            var output = new GetQualificationForViewDto { Qualification = ObjectMapper.Map<QualificationDto>(qualification) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Qualification_Edit)]
        public virtual async Task<GetQualificationForEditOutput> GetQualificationForEdit(EntityDto<Guid> input)
        {
            var qualification = await _qualificationRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetQualificationForEditOutput { Qualification = ObjectMapper.Map<CreateOrEditQualificationDto>(qualification) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditQualificationDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Qualification_Create)]
        protected virtual async Task Create(CreateOrEditQualificationDto input)
        {
            var qualification = ObjectMapper.Map<Qualification>(input);

            if (AbpSession.TenantId != null)
            {
                qualification.TenantId = (int?)AbpSession.TenantId;
            }

            await _qualificationRepository.InsertAsync(qualification);

        }

        [AbpAuthorize(AppPermissions.Pages_Qualification_Edit)]
        protected virtual async Task Update(CreateOrEditQualificationDto input)
        {
            var qualification = await _qualificationRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, qualification);

        }

        [AbpAuthorize(AppPermissions.Pages_Qualification_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _qualificationRepository.DeleteAsync(input.Id);
        }

    }
}