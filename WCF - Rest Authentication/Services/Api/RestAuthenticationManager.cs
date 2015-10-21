using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Web;
using WcfRestAuthentication.Authentication;

namespace WcfRestAuthentication.Services.Api
{
    public class RestAuthenticationManager : ServiceAuthenticationManager
    {
        private readonly IAuthenticationProvider _authenticationProvider;

        public RestAuthenticationManager() { }

        public RestAuthenticationManager(IAuthenticationProvider authenticationProvider)
        {
            _authenticationProvider = authenticationProvider;
        }

        public override ReadOnlyCollection<IAuthorizationPolicy> Authenticate(ReadOnlyCollection<IAuthorizationPolicy> authPolicy, Uri listenUri, ref Message message)
        {
            var requestProperties =
                (HttpRequestMessageProperty)message.Properties[HttpRequestMessageProperty.Name];

            var rawAuthHeader = requestProperties.Headers["Authorization"];

            BasicAuthenticationHeaderTranslator authHeader = null;

            //Secure by default... if no authentication provider is configured, no entry.
            if (_authenticationProvider != null && BasicAuthenticationHeaderTranslator.TryDecode(rawAuthHeader, out authHeader)) ;
            {
                Thread.CurrentPrincipal = _authenticationProvider.Authenticate(authHeader.Username, authHeader.Password);

                var httpContext = new HttpContextWrapper(HttpContext.Current) { User = Thread.CurrentPrincipal };
                if (httpContext.User != null)
                    return authPolicy;
            }

            SendUnauthorizedResponse();

            return base.Authenticate(authPolicy, listenUri, ref message);
        }

        private void SendUnauthorizedResponse()
        {
            HttpContext.Current.Response.StatusCode = 401;
            HttpContext.Current.Response.StatusDescription = "Unauthorized";

            foreach (var header in _authenticationProvider.GetUnauthenticatedHttpHeaders())
            {
                HttpContext.Current.Response.Headers.Add(header.Key, header.Value);
            }

            HttpContext.Current.Response.End();
        }
    }
}