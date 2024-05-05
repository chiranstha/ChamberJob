using Suktas.Payroll.DashboardCustomization;
using Suktas.Payroll.DashboardCustomization.Dto;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Models.CustomizableDashboard
{
    public class CustomizableDashboardViewModel
    {
        public DashboardOutput DashboardOutput { get; }

        public Dashboard UserDashboard { get; }

        public CustomizableDashboardViewModel(
            DashboardOutput dashboardOutput,
            Dashboard userDashboard)
        {
            DashboardOutput = dashboardOutput;
            UserDashboard = userDashboard;
        }
    }
}