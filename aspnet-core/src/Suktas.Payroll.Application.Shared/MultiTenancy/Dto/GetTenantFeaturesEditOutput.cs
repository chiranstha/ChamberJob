using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Suktas.Payroll.Editions.Dto;

namespace Suktas.Payroll.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}