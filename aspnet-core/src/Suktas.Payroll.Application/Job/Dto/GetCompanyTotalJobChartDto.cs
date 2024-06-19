using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suktas.Payroll.Job.Dto
{
    public class GetCompanyTotalJobChartDto
    {
        public List<string> Category { get; set; }
        public List<int> Data { get; set; }
    }
}
