using System.Collections.Generic;
using Suktas.Payroll.DynamicEntityProperties.Dto;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Models.DynamicProperty
{
    public class CreateOrEditDynamicPropertyViewModel
    {
        public DynamicPropertyDto DynamicPropertyDto { get; set; }

        public List<string> AllowedInputTypes { get; set; }
    }
}
