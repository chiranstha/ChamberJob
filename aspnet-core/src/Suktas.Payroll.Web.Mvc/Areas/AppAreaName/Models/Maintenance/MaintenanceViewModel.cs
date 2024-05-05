using System.Collections.Generic;
using Suktas.Payroll.Caching.Dto;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
        
        public bool CanClearAllCaches { get; set; }
    }
}