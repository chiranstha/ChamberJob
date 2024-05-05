using Abp.Dependency;

namespace Suktas.Payroll.Web.Xss
{
    public interface IHtmlSanitizer: ITransientDependency
    {
        string Sanitize(string html);
    }
}