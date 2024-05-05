using System.ComponentModel.DataAnnotations;

namespace Suktas.Payroll.Web.Models.Account
{
    public class SendPasswordResetLinkViewModel
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}