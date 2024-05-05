using System.Collections.Generic;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Suktas.Payroll.Authorization.Users.Importing.Dto;
using Suktas.Payroll.DataExporting.Excel.MiniExcel;
using Suktas.Payroll.Dto;
using Suktas.Payroll.Storage;

namespace Suktas.Payroll.Authorization.Users.Importing
{
    public class InvalidUserExporter : MiniExcelExcelExporterBase, IInvalidUserExporter, ITransientDependency
    {
        public InvalidUserExporter(ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<ImportUserDto> userList)
        {
            var items = new List<Dictionary<string, object>>();

            foreach (var user in userList)
            {
                items.Add(new Dictionary<string, object>()
                {
                    {L("UserName"), user.UserName},
                    {L("Name"), user.Name},
                    {L("Surname"), user.Surname},
                    {L("EmailAddress"), user.EmailAddress},
                    {L("PhoneNumber"), user.PhoneNumber},
                    {L("Password"), user.Password},
                    {L("Roles"), user.AssignedRoleNames?.JoinAsString(",")},
                    {L("Refuse Reason"), user.Exception}, //TODO@MiniExcel -> localize
                });
            }

            return CreateExcelPackage("InvalidUserImportList.xlsx", items);
        }
    }
}
