using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Suktas.Payroll.Startup
{
    [DependsOn(typeof(PayrollCoreModule))]
    public class PayrollGraphQLModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PayrollGraphQLModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}