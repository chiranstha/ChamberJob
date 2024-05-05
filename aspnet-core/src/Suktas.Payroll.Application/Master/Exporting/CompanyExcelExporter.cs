using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Suktas.Payroll.DataExporting.Excel.NPOI;
using Suktas.Payroll.Master.Dtos;
using Suktas.Payroll.Dto;
using Suktas.Payroll.Storage;

namespace Suktas.Payroll.Master.Exporting
{
    public class CompanyExcelExporter : NpoiExcelExporterBase, ICompanyExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public CompanyExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetCompanyForViewDto> company)
        {
            return CreateExcelPackage(
                    "Company.xlsx",
                    excelPackage =>
                    {

                        var sheet = excelPackage.CreateSheet(L("Company"));

                        AddHeader(
                            sheet,
                        L("Name"),
                        L("Address"),
                        L("AuthorizedPerson"),
                        L("ContactNo"),
                        L("BusinessNature"),
                        L("EstablishedYear"),
                        (L("CompanyCategory")) + L("Name"),
                        (L("CompanyType")) + L("Name")
                            );

                        AddObjects(
                            sheet,  company,
                        _ => _.Company.Name,
                        _ => _.Company.Address,
                        _ => _.Company.AuthorizedPerson,
                        _ => _.Company.ContactNo,
                        _ => _.Company.BusinessNature,
                        _ => _.Company.EstablishedYear,
                        _ => _.CompanyCategoryName,
                        _ => _.CompanyTypeName
                            );

                    });

        }
    }
}