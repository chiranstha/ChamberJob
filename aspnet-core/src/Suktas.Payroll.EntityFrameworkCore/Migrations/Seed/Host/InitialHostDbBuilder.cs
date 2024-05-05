using Suktas.Payroll.EntityFrameworkCore;

namespace Suktas.Payroll.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly PayrollDbContext _context;

        public InitialHostDbBuilder(PayrollDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
