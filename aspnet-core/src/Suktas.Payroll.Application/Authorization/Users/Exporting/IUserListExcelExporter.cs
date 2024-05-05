using System.Collections.Generic;
using Suktas.Payroll.Authorization.Users.Dto;
using Suktas.Payroll.Dto;

namespace Suktas.Payroll.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}