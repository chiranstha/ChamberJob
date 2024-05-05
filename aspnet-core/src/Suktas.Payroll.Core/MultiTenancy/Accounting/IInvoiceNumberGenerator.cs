using System.Threading.Tasks;
using Abp.Dependency;

namespace Suktas.Payroll.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}