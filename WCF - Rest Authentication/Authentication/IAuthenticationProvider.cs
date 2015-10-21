using System.Collections.Generic;
using System.Security.Principal;

namespace WcfRestAuthentication.Authentication
{
    public interface IAuthenticationProvider
    {
        string AuthenticationType { get; }

        string Realm { get; }

        IPrincipal Authenticate(string username, string password);

        Dictionary<string, string> GetUnauthenticatedHttpHeaders();
    }
}