using Abp.AutoMapper;
using Suktas.Payroll.Localization.Dto;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Models.Languages
{
    [AutoMapFrom(typeof(GetLanguageForEditOutput))]
    public class CreateOrEditLanguageModalViewModel : GetLanguageForEditOutput
    {
        public bool IsEditMode => Language.Id.HasValue;
    }
}