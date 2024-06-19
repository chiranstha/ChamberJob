using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suktas.Payroll.Job.Dto
{
    public class GetCompanyWiseJobDto
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

     

       

        public string CompanyName { get; set; }

        public string JobSkillName { get; set; }
        public int RequiredQty { get; set; }
    }
}
