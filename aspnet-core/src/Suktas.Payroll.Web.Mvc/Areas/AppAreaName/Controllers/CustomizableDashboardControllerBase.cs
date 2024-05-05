using Microsoft.AspNetCore.Mvc;
using Suktas.Payroll.DashboardCustomization;
using Suktas.Payroll.DashboardCustomization.Dto;
using Suktas.Payroll.Web.Areas.AppAreaName.Models.CustomizableDashboard;
using Suktas.Payroll.Web.Controllers;
using System.Linq;
using System.Threading.Tasks;
using Suktas.Payroll.Web.Areas.AppAreaName.Startup;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Controllers
{
    public abstract class CustomizableDashboardControllerBase : PayrollControllerBase
    {
        protected readonly IDashboardCustomizationAppService DashboardCustomizationAppService;
        protected readonly DashboardViewConfiguration DashboardViewConfiguration;

        protected CustomizableDashboardControllerBase(
            DashboardViewConfiguration dashboardViewConfiguration,
            IDashboardCustomizationAppService dashboardCustomizationAppService)
        {
            DashboardViewConfiguration = dashboardViewConfiguration;
            DashboardCustomizationAppService = dashboardCustomizationAppService;
        }

        public async Task<PartialViewResult> AddWidgetModal(string dashboardName, string pageId)
        {
            var availableWidgets = await DashboardCustomizationAppService.GetAllAvailableWidgetDefinitionsForPage(
                new GetAvailableWidgetDefinitionsForPageInput()
                {
                    Application = PayrollDashboardCustomizationConsts.Applications.Mvc,
                    PageId = pageId,
                    DashboardName = dashboardName
                });

            var viewModel = new AddWidgetViewModel
            {
                Widgets = availableWidgets,
                DashboardName = dashboardName,
                PageId = pageId
            };

            return PartialView(
                "~/Areas/AppAreaName/Views/Shared/Components/CustomizableDashboard/_AddWidgetModal.cshtml",
                viewModel
            );
        }

        protected async Task<ActionResult> GetView(string dashboardName)
        {
            var dashboardDefinition = DashboardCustomizationAppService.GetDashboardDefinition(
                new GetDashboardInput
                {
                    DashboardName = dashboardName,
                    Application = PayrollDashboardCustomizationConsts.Applications.Mvc
                }
            );

            var userDashboard = await DashboardCustomizationAppService.GetUserDashboard(new GetDashboardInput
                {
                    DashboardName = dashboardName,
                    Application = PayrollDashboardCustomizationConsts.Applications.Mvc
                }
            );

            // Show only view defined widgets
            foreach (var userDashboardPage in userDashboard.Pages)
            {
                userDashboardPage.Widgets = userDashboardPage.Widgets
                    .Where(w => DashboardViewConfiguration.WidgetViewDefinitions.ContainsKey(w.WidgetId)).ToList();
            }

            dashboardDefinition.Widgets = dashboardDefinition.Widgets.Where(dw =>
                userDashboard.Pages.Any(p => p.Widgets.Select(w => w.WidgetId).Contains(dw.Id))).ToList();

            return View("~/Areas/AppAreaName/Views/Shared/Components/CustomizableDashboard/Index.cshtml",
                new CustomizableDashboardViewModel(
                    dashboardDefinition,
                    userDashboard)
            );
        }
    }
}
