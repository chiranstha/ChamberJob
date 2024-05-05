using System.Collections.Generic;
using Abp.Localization;
using Suktas.Payroll.Install.Dto;

namespace Suktas.Payroll.Web.Models.Install
{
    public class InstallViewModel
    {
        public List<ApplicationLanguage> Languages { get; set; }

        public AppSettingsJsonDto AppSettingsJson { get; set; }
    }
}
