using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Suktas.Payroll.Authorization;

namespace Suktas.Payroll
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(PayrollApplicationSharedModule),
        typeof(PayrollCoreModule)
        )]
    public class PayrollApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PayrollApplicationModule).GetAssembly());
        }
    }
}