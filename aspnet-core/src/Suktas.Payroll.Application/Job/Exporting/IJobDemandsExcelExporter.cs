﻿using System.Collections.Generic;
using Suktas.Payroll.Job.Dtos;
using Suktas.Payroll.Dto;

namespace Suktas.Payroll.Job.Exporting
{
    public interface IJobDemandsExcelExporter
    {
        FileDto ExportToFile(List<GetJobDemandForViewDto> jobDemands);
    }
}