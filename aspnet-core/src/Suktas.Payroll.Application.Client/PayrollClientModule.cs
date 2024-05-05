using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Suktas.Payroll
{
    public class PayrollClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PayrollClientModule).GetAssembly());
        }
    }
}
