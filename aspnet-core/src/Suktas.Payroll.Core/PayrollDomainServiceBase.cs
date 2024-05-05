using Abp.Domain.Services;

namespace Suktas.Payroll
{
    public abstract class PayrollDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected PayrollDomainServiceBase()
        {
            LocalizationSourceName = PayrollConsts.LocalizationSourceName;
        }
    }
}
