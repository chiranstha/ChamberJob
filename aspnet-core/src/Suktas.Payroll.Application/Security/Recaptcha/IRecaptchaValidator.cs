using System.Threading.Tasks;

namespace Suktas.Payroll.Security.Recaptcha
{
    public interface IRecaptchaValidator
    {
        Task ValidateAsync(string captchaResponse);
    }
}