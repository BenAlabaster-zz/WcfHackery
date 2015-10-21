using System;
using System.Linq;
using System.Security.Principal;
using WcfRestAuthentication.Data;

namespace WcfRestAuthentication.Authentication
{
    public class MyFakeAuthenticationProvider : BasicAuthenticationProvider
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfigurationProvider _configurationProvider;

        public override string Realm
        {
            get
            {
                return _configurationProvider.GetMandatoryAppSetting<string>(
                    ApplicationConstants.ConfigurationKeys.Api.Security.Realm);
            }
        }

        public MyFakeAuthenticationProvider(IUserRepository userRepository, IConfigurationProvider configurationProvider)
        {
            _userRepository = userRepository;
            _configurationProvider = configurationProvider;
        }

        public override IPrincipal Authenticate(string username, string password)
        {
            var user =
                _userRepository.Query(
                    u =>
                        u.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
                        CheckPassword(u, password)).FirstOrDefault();

            if (user == null)
                return null;

            return new GenericPrincipal(new GenericIdentity(user.UserName), user.Roles.ToArray());
        }

        private bool CheckPassword(Model.User user, string password)
        {
            return true;
        }
    }
}