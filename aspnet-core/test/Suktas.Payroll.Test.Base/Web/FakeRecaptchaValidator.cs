using System.Threading.Tasks;
using Suktas.Payroll.Security.Recaptcha;

namespace Suktas.Payroll.Test.Base.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}
