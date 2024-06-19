using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Suktas.Payroll.Job.Dto;
using Suktas.Payroll.Master;
using Suktas.Payroll.Storage;

namespace Suktas.Payroll.Job;

[AbpAuthorize]
public class JobDashboardAppService : PayrollAppServiceBase
{
    private readonly IBinaryObjectManager _binaryObjectManager;
    private readonly IRepository<JobApply, Guid> _jobApplyRepository;
    private readonly IRepository<JobDemand, Guid> _jobDemandRepository;
    private readonly IRepository<Company, int> _lookupCompanyRepository;
    private readonly IRepository<Employee, Guid> _lookupEmployeeRepository;
    private readonly IRepository<JobSkill, Guid> _lookupJobSkillRepository;

    private readonly ITempFileCacheManager _tempFileCacheManager;

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
    public virtual async Task<GetJobStatsDto> GetJobStats()
    {

        var companyTotal = await _lookupCompanyRepository.CountAsync();

        var filteredJobDemands = await _jobDemandRepository.GetAll()
            .Include(e => e.CompanyFk)
            .Include(e => e.JobSkillFk)
            .Select(x => new
            {
                x.CompanyId,
                x.JobSkillId,
                x.Id,
                x.RequiredQty
            })
            .ToListAsync();


        return new GetJobStatsDto
        {
            TotalCompany = companyTotal,
            TotalJob = filteredJobDemands.Sum(e => e.RequiredQty),
            TotalJobSkill = filteredJobDemands.DistinctBy(e => e.JobSkillId).Count(),
            TotalJobPost = filteredJobDemands.DistinctBy(e => e.Id).Count()
        };
    }

    [AbpAuthorize]
    public virtual async Task<List<GetCompanyWiseJobDto>> GetCompanyWiseJob()
    {
        var filteredJobDemands = await _jobDemandRepository.GetAll()
            .Include(e => e.CompanyFk)
            .Include(e => e.JobSkillFk)
            .Select(input => new
            {
                input.CompanyId,

                input.Name,
                input.Date,
                input.Address,
                input.RequiredQty,
                CompanyName = input.CompanyFk.Name,
                JobSkil = input.JobSkillFk.Name,

            }).OrderBy(e => e.Date)
            .ToListAsync();

        //foreach (var detail in filteredJobDemands.DistinctBy(e => e.CompanyId).ToList())
        //{
        //    var getCompany = filteredJobDemands.Where(e => e.CompanyId==detail.CompanyId).ToList();
        //    var model = new GetCompanyWiseJobDto
        //    {
        //        Name = detail.Name,
        //        Address = string.Join(',',getCompany.Select(e=>e.Address)),
        //        Date = e.Date,
        //        CompanyName = e.CompanyName,
        //        JobSkillName = e.JobSkil,
        //        RequiredQty = e.RequiredQty
        //    };
        //}


        return filteredJobDemands.Select(e => new GetCompanyWiseJobDto
        {
            Name = e.Name,
            Address = e.Address,
            Date = e.Date,
            CompanyName = e.CompanyName,
            JobSkillName = e.JobSkil,
            RequiredQty = e.RequiredQty
        }).ToList();
    }


    public virtual async Task<GetCompanyWiseJobChartDto> GetCompanyWiseJobChart()
    {
        var filteredJobDemands = await _jobDemandRepository.GetAll()
            .Include(e => e.CompanyFk)
            .Include(e => e.JobSkillFk)
            .Select(input => new
            {
                input.CompanyId,
                input.Name,
                input.Date,
                input.Address,
                input.RequiredQty,
                input.JobSkillId,
                CompanyName = input.CompanyFk.Name,
                JobSkil = input.JobSkillFk.Name,

            }).OrderBy(e => e.CompanyId)
            .ToListAsync();

        var model = new GetCompanyWiseJobChartDto();
        var jobList = new List<GetCompanyWiseSeriesDto>();

        var companyUnique = filteredJobDemands.DistinctBy(x => x.CompanyId).ToList();


        var jobSkilsUnique = filteredJobDemands
            .OrderBy(e => e.JobSkillId)
            .DistinctBy(e => e.JobSkillId).ToList();

        model.Category = companyUnique.Select(x => x.CompanyName).ToList();

        foreach (var jobSkil in jobSkilsUnique.OrderBy(e => e.JobSkillId).ToList())
        {
            var jobTotal = new List<int>();
            foreach (var company in companyUnique)
            {
                var objdata = filteredJobDemands
                    .Where(e => e.CompanyId == company.CompanyId && e.JobSkillId == jobSkil.JobSkillId)
                    .Sum(e => e.RequiredQty);
                jobTotal.Add(objdata);
            }

            var detailModel = new GetCompanyWiseSeriesDto
            {
                Name = jobSkil.JobSkil,
                Data = jobTotal
            };
            jobList.Add(detailModel);
        }

        model.Series = jobList;
        return model;


    }







    public virtual async Task<GetCompanyTotalJobChartDto> GetCompanyTotalJobChart()
    {
        var filteredJobDemands = await _jobDemandRepository.GetAll()
            .Include(e => e.CompanyFk)
            .Include(e => e.JobSkillFk)
            .Select(input => new
            {
                input.CompanyId,
                input.Name,
                input.Date,
                input.Address,
                input.RequiredQty,
                input.JobSkillId,
                CompanyName = input.CompanyFk.Name,
                JobSkil = input.JobSkillFk.Name,

            }).OrderBy(e => e.CompanyId)
            .ToListAsync();

        var model = new GetCompanyTotalJobChartDto();

        var companyUnique = filteredJobDemands.DistinctBy(x => x.CompanyId).ToList();



        model.Category = companyUnique.Select(x => x.CompanyName).ToList();
        var jobTotalList = companyUnique.Select(x => filteredJobDemands.Where(e => e.CompanyId == x.CompanyId)
                .Sum(e => e.RequiredQty))
            .ToList();

        model.Data = jobTotalList;
        return model;


    }







    public virtual async Task<GetCompanyTotalJobChartDto> GetAddressWiseJobChart()
    {
        var filteredJobDemands = await _jobDemandRepository.GetAll()
            .Include(e => e.CompanyFk)
            .Include(e => e.JobSkillFk)
            .Select(input => new
            {
                input.CompanyId,
                input.Name,
                input.Date,
                Address= input.Address.ToLower(),
                input.RequiredQty,
                input.JobSkillId,
                CompanyName = input.CompanyFk.Name,
                JobSkil = input.JobSkillFk.Name,

            }).OrderBy(e => e.Address)
            .ToListAsync();

        var model = new GetCompanyTotalJobChartDto();

        var companyUnique = filteredJobDemands.OrderBy(e => e.Address)
            .DistinctBy(x => x.Address
        ).ToList();



        model.Category = companyUnique.Select(x => x.Address).ToList();
        var jobTotalList = companyUnique.Select(x => filteredJobDemands.Where(e => e.Address == x.Address)
                .Sum(e => e.RequiredQty))
            .ToList();

        model.Data = jobTotalList;
        return model;


    }
}