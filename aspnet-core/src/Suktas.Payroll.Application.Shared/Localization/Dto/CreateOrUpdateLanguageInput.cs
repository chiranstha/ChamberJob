using System.ComponentModel.DataAnnotations;

namespace Suktas.Payroll.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}