using System.ComponentModel.DataAnnotations;

namespace Suktas.Payroll.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
