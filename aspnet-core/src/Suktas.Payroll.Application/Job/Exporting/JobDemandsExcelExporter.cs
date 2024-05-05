using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Suktas.Payroll.DataExporting.Excel.NPOI;
using Suktas.Payroll.Job.Dtos;
using Suktas.Payroll.Dto;
using Suktas.Payroll.Storage;

namespace Suktas.Payroll.Job.Exporting
{
    public class JobDemandsExcelExporter : NpoiExcelExporterBase, IJobDemandsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public JobDemandsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetJobDemandForViewDto> jobDemands)
        {
            return CreateExcelPackage(
                    "JobDemands.xlsx",
                    excelPackage =>
                    {

                        var sheet = excelPackage.CreateSheet(L("JobDemands"));

                        AddHeader(
                            sheet,
                        L("Name"),
                        L("Address"),
                        L("Date"),
                        L("Salary"),
                        L("ExpiredDate"),
                        (L("Company")) + L("Name"),
                        (L("JobSkill")) + L("Name")
                            );

                        AddObjects(
                            sheet,  jobDemands,
                        _ => _.JobDemand.Name,
                        _ => _.JobDemand.Address,
                        _ => _timeZoneConverter.Convert(_.JobDemand.Date, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.JobDemand.Salary,
                        _ => _timeZoneConverter.Convert(_.JobDemand.ExpiredDate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.CompanyName,
                        _ => _.JobSkillName
                            );

                        for (var i = 1; i <= jobDemands.Count; i++)
                        {
                            SetCellDataFormat(sheet.GetRow(i).Cells[3 - 1], "yyyy-mm-dd");
                        }
                        sheet.AutoSizeColumn(3 - 1); for (var i = 1; i <= jobDemands.Count; i++)
                        {
                            SetCellDataFormat(sheet.GetRow(i).Cells[5 - 1], "yyyy-mm-dd");
                        }
                        sheet.AutoSizeColumn(5 - 1);
                    });

        }
    }
}