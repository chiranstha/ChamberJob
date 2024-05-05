using System.Globalization;

namespace Suktas.Payroll.Localization
{
    public interface IApplicationCulturesProvider
    {
        CultureInfo[] GetAllCultures();
    }
}