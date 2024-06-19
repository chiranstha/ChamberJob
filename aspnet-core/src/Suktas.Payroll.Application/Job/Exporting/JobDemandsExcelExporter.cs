using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Suktas.Payroll.DataExporting.Excel.NPOI;
using Suktas.Payroll.Dto;
using Suktas.Payroll.Job.Dtos;
using Suktas.Payroll.Storage;

namespace Suktas.Payroll.Job.Exporting;

public class JobDemandsExcelExporter : NpoiExcelExporterBase, IJobDemandsExcelExporter
{
    private readonly IAbpSession _abpSession;

    private readonly ITimeZoneConverter _timeZoneConverter;

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
                    L("CompanyName"),
                    L("JobSector"),
                    L("JobSkill"),
                    L("Address"),
                    L("Qty")
                );

                AddObjects(
                    sheet, jobDemands,
                    d => d.CompanyName,
                    d => d.Name,
                    d => d.JobSkillName,
                    d => d.Address,
                    d => d.RequiredQty
                );
            });
    }
}