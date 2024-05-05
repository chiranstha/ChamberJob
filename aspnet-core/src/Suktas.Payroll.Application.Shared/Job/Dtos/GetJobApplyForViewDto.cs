namespace Suktas.Payroll.Job.Dtos
{
    public class GetJobApplyForViewDto
    {
        public JobApplyDto JobApply { get; set; }

        public string CompanyName { get; set; }

        public string JobDemandName { get; set; }

        public string EmployeeName { get; set; }

    }
}