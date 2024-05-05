using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Suktas.Payroll.Editions.Dto;
using Suktas.Payroll.Web.Areas.AppAreaName.Models.Common;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Models.Editions
{
    [AutoMapFrom(typeof(GetEditionEditOutput))]
    public class EditEditionModalViewModel : GetEditionEditOutput, IFeatureEditViewModel
    {
        public bool IsEditMode => Edition.Id.HasValue;

        public IReadOnlyList<ComboboxItemDto> EditionItems { get; set; }

        public IReadOnlyList<ComboboxItemDto> FreeEditionItems { get; set; }
    }
}