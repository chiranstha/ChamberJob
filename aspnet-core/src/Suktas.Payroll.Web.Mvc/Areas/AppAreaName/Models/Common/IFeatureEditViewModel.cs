using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Suktas.Payroll.Editions.Dto;

namespace Suktas.Payroll.Web.Areas.AppAreaName.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}