using System.Collections.Generic;
using Suktas.Payroll.Authorization.Users.Importing.Dto;
using Suktas.Payroll.Dto;

namespace Suktas.Payroll.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
