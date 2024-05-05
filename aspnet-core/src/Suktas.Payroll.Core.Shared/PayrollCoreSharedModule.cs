using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Suktas.Payroll
{
    public class PayrollCoreSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PayrollCoreSharedModule).GetAssembly());
        }
    }
}