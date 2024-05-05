using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Suktas.Payroll.ApiClient;
using Suktas.Payroll.Mobile.MAUI.Core.ApiClient;

namespace Suktas.Payroll
{
    [DependsOn(typeof(PayrollClientModule), typeof(AbpAutoMapperModule))]

    public class PayrollMobileMAUIModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;

            Configuration.ReplaceService<IApplicationContext, MAUIApplicationContext>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PayrollMobileMAUIModule).GetAssembly());
        }
    }
}