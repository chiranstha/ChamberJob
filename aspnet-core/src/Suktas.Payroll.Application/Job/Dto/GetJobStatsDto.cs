using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suktas.Payroll.Job.Dto
{
    public class GetJobStatsDto
    {
        public int TotalCompany { get; set; }
        public int TotalJob  { get; set; }
        public int TotalJobSkill  { get; set; }
        public int TotalJobPost { get; set; }
    }
}
