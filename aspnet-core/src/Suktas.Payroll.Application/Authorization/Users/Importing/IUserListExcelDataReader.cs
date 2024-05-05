using System.Collections.Generic;
using Suktas.Payroll.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace Suktas.Payroll.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
