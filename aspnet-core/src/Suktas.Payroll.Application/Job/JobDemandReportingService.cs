using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using Suktas.Payroll.Authorization;
using Suktas.Payroll.Dto;
using Suktas.Payroll.Job.Dtos;
using Suktas.Payroll.Job.Exporting;
using Suktas.Payroll.Master;

namespace Suktas.Payroll.Job;

[AbpAuthorize(AppPermissions.Pages_JobDemands)]
public class JobDemandReportingAppService : PayrollAppServiceBase
{
    private readonly IRepository<JobDemand, Guid> _jobDemandRepository;
    private readonly IJobDemandsExcelExporter _jobDemandsExcelExporter;
    private readonly IRepository<Company, int> _lookupCompanyRepository;
    private readonly IRepository<JobSkill, Guid> _lookupJobSkillRepository;

    public JobDemandReportingAppService(
        IRepository<JobDemand, Guid> jobDemandRepository,
        IJobDemandsExcelExporter jobDemandsExcelExporter,
        IRepository<Company, int> lookupCompanyRepository,
        IRepository<JobSkill, Guid> lookupJobSkillRepository)
    {
        _jobDemandRepository = jobDemandRepository;
        _jobDemandsExcelExporter = jobDemandsExcelExporter;
        _lookupCompanyRepository = lookupCompanyRepository;
        _lookupJobSkillRepository = lookupJobSkillRepository;
    }


    public virtual async Task<List<GetAllJobDemandReportDto>> GetAllDemand()
    {
        var filteredJobDemands = await _jobDemandRepository.GetAll()
            .AsNoTracking()
            .Include(e => e.CompanyFk)
            .Include(e => e.JobSkillFk)
            .ToListAsync();


        var results = filteredJobDemands.Select(o => new GetAllJobDemandReportDto
            {
                Id = o.Id,
                Name = o.Name,
                Address = o.Address,
                Date = o.Date,
                Salary = o.Salary,
                RequiredQty = o.RequiredQty,
                ExpiredDate = o.ExpiredDate,
                CompanyName = o.CompanyFk.Name,
                JobSkillName = o.JobSkillFk.Name
            })
            .ToList();

        return results;
    }


    public virtual async Task<FileDto> GetJobDemandsToExcel(GetAllJobDemandsForExcelInput input)
    {
        var filteredJobDemands = _jobDemandRepository.GetAll()
            .Include(e => e.CompanyFk)
            .Include(e => e.JobSkillFk)
            .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                e => false || e.Name.Contains(input.Filter) || e.Address.Contains(input.Filter) ||
                     e.Salary.Contains(input.Filter) || e.JobSpecification.Contains(input.Filter) ||
                     e.Description.Contains(input.Filter))
            .WhereIf(!string.IsNullOrWhiteSpace(input.CompanyNameFilter),
                e => e.CompanyFk != null && e.CompanyFk.Name == input.CompanyNameFilter)
            .WhereIf(!string.IsNullOrWhiteSpace(input.JobSkillNameFilter),
                e => e.JobSkillFk != null && e.JobSkillFk.Name == input.JobSkillNameFilter);

        var query = from o in filteredJobDemands
            join o1 in _lookupCompanyRepository.GetAll() on o.CompanyId equals o1.Id into j1
            from s1 in j1.DefaultIfEmpty()
            join o2 in _lookupJobSkillRepository.GetAll() on o.JobSkillId equals o2.Id into j2
            from s2 in j2.DefaultIfEmpty()
            select new GetJobDemandForViewDto
            {
                Name = o.Name,
                Address = o.Address,
                Date = o.Date,
                Salary = o.Salary,
                ExpiredDate = o.ExpiredDate,
                Id = o.Id,
                CompanyName = s1 == null || s1.Name == null ? "" : s1.Name,
                JobSkillName = s2 == null || s2.Name == null ? "" : s2.Name
            };

        var jobDemandListDtos = await query.ToListAsync();

        return _jobDemandsExcelExporter.ExportToFile(jobDemandListDtos);
    }

    [AbpAuthorize(AppPermissions.Pages_JobDemands)]
    public async Task<List<JobDemandCompanyLookupTableDto>> GetAllCompanyForTableDropdown()
    {
        return await _lookupCompanyRepository.GetAll().Where(e => e.UserId == AbpSession.GetUserId())
            .Select(company => new JobDemandCompanyLookupTableDto
            {
                Id = company.Id,
                DisplayName = company == null || company.Name == null ? "" : company.Name.ToString()
            }).ToListAsync();
    }

    [AbpAuthorize(AppPermissions.Pages_JobDemands)]
    public async Task<List<JobDemandJobSkillLookupTableDto>> GetAllJobSkillForTableDropdown()
    {
        return await _lookupJobSkillRepository.GetAll()
            .Select(jobSkill => new JobDemandJobSkillLookupTableDto
            {
                Id = jobSkill.Id,
                DisplayName = jobSkill == null || jobSkill.Name == null ? "" : jobSkill.Name.ToString()
            }).ToListAsync();
    }
}