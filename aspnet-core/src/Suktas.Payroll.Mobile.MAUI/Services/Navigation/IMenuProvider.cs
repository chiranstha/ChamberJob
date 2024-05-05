using Suktas.Payroll.Models.NavigationMenu;

namespace Suktas.Payroll.Services.Navigation
{
    public interface IMenuProvider
    {
        List<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}