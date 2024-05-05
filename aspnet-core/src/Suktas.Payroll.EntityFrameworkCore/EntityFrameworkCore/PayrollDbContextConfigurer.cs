using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Suktas.Payroll.EntityFrameworkCore
{
    public static class PayrollDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<PayrollDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<PayrollDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}