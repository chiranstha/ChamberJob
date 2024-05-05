using System.Threading.Tasks;

namespace Suktas.Payroll.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}