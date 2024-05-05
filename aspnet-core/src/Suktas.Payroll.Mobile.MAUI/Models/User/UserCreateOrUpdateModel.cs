using Abp.AutoMapper;
using Suktas.Payroll.Authorization.Users.Dto;

namespace Suktas.Payroll.Mobile.MAUI.Models.User
{
    [AutoMapFrom(typeof(CreateOrUpdateUserInput))]
    public class UserCreateOrUpdateModel : CreateOrUpdateUserInput
    {

    }
}
