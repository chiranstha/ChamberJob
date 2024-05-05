using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Suktas.Payroll.MultiTenancy.HostDashboard.Dto;

namespace Suktas.Payroll.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}