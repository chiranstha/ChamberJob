using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Suktas.Payroll.DataExporting.Excel.NPOI;
using Suktas.Payroll.Job.Dtos;
using Suktas.Payroll.Dto;
using Suktas.Payroll.Storage;

namespace Suktas.Payroll.Job.Exporting
{
    public class EmployeeExcelExporter : NpoiExcelExporterBase, IEmployeeExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public EmployeeExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetEmployeeForViewDto> employee)
        {
            return CreateExcelPackage(
                    "Employee.xlsx",
                    excelPackage =>
                    {

                        var sheet = excelPackage.CreateSheet(L("Employee"));

                        AddHeader(
                            sheet,
                        L("Name"),
                        L("PhoneNo"),
                        L("Gender"),
                        L("Dbo"),
                        L("Qualification"),
                        L("ExpectedSalary"),
                        (L("JobSkill")) + L("Name")
                            );

                        AddObjects(
                            sheet,  employee,
                        _ => _.Name,
                        _ => _.PhoneNo,
                        _ => _.Gender,
                        _ => _timeZoneConverter.Convert(_.Dbo, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Qualification,
                        _ => _.ExpectedSalary,
                        _ => _.JobSkillName
                            );

                        for (var i = 1; i <= employee.Count; i++)
                        {
                            SetCellDataFormat(sheet.GetRow(i).Cells[4 - 1], "yyyy-mm-dd");
                        }
                        sheet.AutoSizeColumn(4 - 1);
                    });

        }
    }
}