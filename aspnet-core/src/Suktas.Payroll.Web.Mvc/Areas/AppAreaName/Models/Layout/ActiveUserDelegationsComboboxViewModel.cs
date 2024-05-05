using System.Collections.Generic;
using Suktas.Payroll.Authorization.Delegation;
using Suktas.Payroll.Authorization.Users.Delegation.Dto;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Models.Layout
{
    public class ActiveUserDelegationsComboboxViewModel
    {
        public IUserDelegationConfiguration UserDelegationConfiguration { get; set; }

        public List<UserDelegationDto> UserDelegations { get; set; }

        public string CssClass { get; set; }
    }
}
