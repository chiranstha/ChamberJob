﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Suktas.Payroll.EntityFrameworkCore;

namespace Suktas.Payroll.HealthChecks
{
    public class PayrollDbContextHealthCheck : IHealthCheck
    {
        private readonly DatabaseCheckHelper _checkHelper;

        public PayrollDbContextHealthCheck(DatabaseCheckHelper checkHelper)
        {
            _checkHelper = checkHelper;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_checkHelper.Exist("db"))
            {
                return Task.FromResult(HealthCheckResult.Healthy("PayrollDbContext connected to database."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("PayrollDbContext could not connect to database"));
        }
    }
}
