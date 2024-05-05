using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Suktas.Payroll.Configure;
using Suktas.Payroll.Startup;
using Suktas.Payroll.Test.Base;

namespace Suktas.Payroll.GraphQL.Tests
{
    [DependsOn(
        typeof(PayrollGraphQLModule),
        typeof(PayrollTestBaseModule))]
    public class PayrollGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PayrollGraphQLTestModule).GetAssembly());
        }
    }
}