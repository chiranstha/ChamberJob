using System.ComponentModel.DataAnnotations;

namespace Suktas.Payroll.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}