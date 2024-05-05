using System.Collections.Generic;
using Suktas.Payroll.Master.Dtos;
using Suktas.Payroll.Dto;

namespace Suktas.Payroll.Master.Exporting
{
    public interface ICompanyExcelExporter
    {
        FileDto ExportToFile(List<GetCompanyForViewDto> company);
    }
}