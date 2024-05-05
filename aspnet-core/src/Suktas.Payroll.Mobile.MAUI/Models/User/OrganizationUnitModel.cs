using Abp.AutoMapper;
using Suktas.Payroll.Organizations.Dto;

namespace Suktas.Payroll.Mobile.MAUI.Models.User
{
    [AutoMapFrom(typeof(OrganizationUnitDto))]
    public class OrganizationUnitModel : OrganizationUnitDto
    {
        public bool IsAssigned { get; set; }
    }
}
