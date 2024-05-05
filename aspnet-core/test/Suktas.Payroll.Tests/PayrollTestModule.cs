using Abp.Modules;
using Suktas.Payroll.Test.Base;

namespace Suktas.Payroll.Tests
{
    [DependsOn(typeof(PayrollTestBaseModule))]
    public class PayrollTestModule : AbpModule
    {
       
    }
}
