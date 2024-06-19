using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Suktas.Payroll.Authorization;
using Suktas.Payroll.Job.Dtos;
using Suktas.Payroll.Master;
using Suktas.Payroll.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Suktas.Payroll.Job
{
    [AbpAuthorize]
    public class JobDashboardAppService : PayrollAppServiceBase
    {
        private readonly IRepository<JobApply, Guid> _jobApplyRepository;
        private readonly IRepository<Company, int> _lookupCompanyRepository;
        private readonly IRepository<JobDemand, Guid> _jobDemandRepository;
        private readonly IRepository<Employee, Guid> _lookupEmployeeRepository;
        private readonly IRepository<JobSkill, Guid> _lookupJobSkillRepository;

        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;

        public JobDashboardAppService(
            IRepository<JobApply, Guid> jobApplyRepository,
            IRepository<Company, int> lookupCompanyRepository, 
            IRepository<JobDemand, Guid> lookupJobDemandRepository, 
            IRepository<Employee, Guid> lookupEmployeeRepository, 
            ITempFileCacheManager tempFileCacheManager, 
            IBinaryObjectManager binaryObjectManager, 
            IRepository<JobSkill, Guid> lookupJobSkillRepository)
        {
            _jobApplyRepository = jobApplyRepository;
            _lookupCompanyRepository = lookupCompanyRepository;
            _jobDemandRepository = lookupJobDemandRepository;
            _lookupEmployeeRepository = lookupEmployeeRepository;

            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;
            _lookupJobSkillRepository = lookupJobSkillRepository;
        }


        [AbpAuthorize]
        public virtual async Task<GetJobDemandForViewDto> GetJobStats()
        {

            var filteredJobDemands = await _jobDemandRepository.GetAll()
                .Include(e => e.CompanyFk)
                .Include(e => e.JobSkillFk).ToListAsync();


        }

       

    }
}