using Microsoft.Extensions.Configuration;

namespace Suktas.Payroll.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
