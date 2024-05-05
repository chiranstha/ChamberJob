using System.Threading.Tasks;

namespace Suktas.Payroll.Security
{
    public interface IPasswordComplexitySettingStore
    {
        Task<PasswordComplexitySetting> GetSettingsAsync();
    }
}
