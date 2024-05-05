using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Suktas.Payroll.Web.Authentication.JwtBearer
{
    public class AsyncJwtBearerOptions : JwtBearerOptions
    {
        public readonly List<IAsyncSecurityTokenValidator> AsyncSecurityTokenValidators;
        
        private readonly PayrollAsyncJwtSecurityTokenHandler _defaultAsyncHandler = new PayrollAsyncJwtSecurityTokenHandler();

        public AsyncJwtBearerOptions()
        {
            AsyncSecurityTokenValidators = new List<IAsyncSecurityTokenValidator>() {_defaultAsyncHandler};
        }
    }

}
