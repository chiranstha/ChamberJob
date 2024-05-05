using System.Collections.Generic;
using Suktas.Payroll.DashboardCustomization.Dto;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Models.CustomizableDashboard
{
    public class AddWidgetViewModel
    {
        public List<WidgetOutput> Widgets { get; set; }

        public string DashboardName { get; set; }

        public string PageId { get; set; }
    }
}
