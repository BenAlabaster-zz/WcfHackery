using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web.Services.Description;
using WcfRestAuthentication.Authentication;
using WcfRestAuthentication.Data;

namespace WcfRestAuthentication.Services.Api
{
    public class ApiServiceHostFactory : ServiceHostFactory
    {
        private IUserRepository UserRepository { get; set; }
        private IProductRepository ProductRepository { get; set; }
        private IConfigurationProvider ConfigurationProvider { get; set; }
        private ServiceAuthenticationManager AuthenticationManager { get; set; }
        private ServiceAuthorizationManager AuthorizationManager { get; set; }

        public ApiServiceHostFactory(IConfigurationProvider configurationProvider, IUserRepository userRepository, IProductRepository productRepository, 
            ServiceAuthenticationManager authenticationManager, ServiceAuthorizationManager authorizationManager)
        {
            UserRepository = userRepository;
            ProductRepository = productRepository;
            ConfigurationProvider = configurationProvider;
            AuthenticationManager = authenticationManager;
            AuthorizationManager = authorizationManager;
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            if (serviceType == typeof(ApiService))
            {
                return new ApiServiceHost(UserRepository, ProductRepository, ConfigurationProvider, AuthenticationManager, AuthorizationManager, serviceType, baseAddresses);
            }
            return base.CreateServiceHost(serviceType, baseAddresses);
        }
    }
}