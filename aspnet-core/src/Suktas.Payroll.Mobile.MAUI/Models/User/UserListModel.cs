using Abp.AutoMapper;
using Suktas.Payroll.Authorization.Users.Dto;

namespace Suktas.Payroll.Mobile.MAUI.Models.User
{
    [AutoMapFrom(typeof(UserListDto))]
    public class UserListModel : UserListDto
    {
        public string Photo { get; set; }

        public string FullName => Name + " " + Surname;
    }
}
