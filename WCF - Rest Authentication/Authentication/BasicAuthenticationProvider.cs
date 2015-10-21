using System.Collections.Generic;
using System.Security.Principal;

namespace WcfRestAuthentication.Authentication
{
    public class BasicAuthenticationProvider : IAuthenticationProvider
    {
        public virtual string AuthenticationType { get { return "Basic"; } }

        public virtual string Realm { get { return "site"; } }

        public virtual IPrincipal Authenticate(string username, string password)
        {
            return null;
        }

        public virtual Dictionary<string, string> GetUnauthenticatedHttpHeaders()
        {
            return new Dictionary<string, string>()
            {
                {"WWW-Authenticate", string.Format("{0} Realm=\"{1}\"", AuthenticationType, Realm)}
            };
        }
    }
}