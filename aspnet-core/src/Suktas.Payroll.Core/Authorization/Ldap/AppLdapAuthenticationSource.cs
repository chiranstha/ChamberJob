using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using Suktas.Payroll.Authorization.Users;
using Suktas.Payroll.MultiTenancy;

namespace Suktas.Payroll.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}