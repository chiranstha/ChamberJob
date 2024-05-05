using Abp.Dependency;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;
using Suktas.Payroll.Configuration;

namespace Suktas.Payroll.Test.Base
{
    public class TestAppConfigurationAccessor : IAppConfigurationAccessor, ISingletonDependency
    {
        public IConfigurationRoot Configuration { get; }

        public TestAppConfigurationAccessor()
        {
            Configuration = AppConfigurations.Get(
                typeof(PayrollTestBaseModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }
    }
}
