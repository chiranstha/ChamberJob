using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Suktas.Payroll
{
    [DependsOn(typeof(PayrollCoreSharedModule))]
    public class PayrollApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PayrollApplicationSharedModule).GetAssembly());
        }
    }
}