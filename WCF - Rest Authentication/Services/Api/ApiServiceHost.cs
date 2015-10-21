using System;
using System.ServiceModel;
using WcfRestAuthentication.Authentication;
using WcfRestAuthentication.Data;
using WcfRestAuthentication.Services.Api.Endpoints.Product;
using WcfRestAuthentication.Services.Api.Endpoints.Product.V1.Behaviors;
using WcfRestAuthentication.Services.Api.Endpoints.User;
using WcfRestAuthentication.Services.Api.Endpoints.User.V1.Behaviors;

namespace WcfRestAuthentication.Services.Api
{
    public class ApiServiceHost : ServiceHost
    {
        private IUserRepository UserRepository { get; set; }
        private IProductRepository ProductRepository { get; set; }
        private IConfigurationProvider ConfigurationProvider { get; set; }
        private ServiceAuthorizationManager AuthorizationManager { get; set; }
        private ServiceAuthenticationManager AuthenticationManager { get; set; }

        public ApiServiceHost(IUserRepository userRepository, IProductRepository productRepository,
            IConfigurationProvider configurationProvider, ServiceAuthenticationManager authenticationManager, ServiceAuthorizationManager authorizationManager,
            Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            UserRepository = userRepository;
            ProductRepository = productRepository;
            ConfigurationProvider = configurationProvider;
            AuthenticationManager = authenticationManager;
            AuthorizationManager = authorizationManager;

            foreach (var cd in ImplementedContracts.Values)
            {
                cd.Behaviors.Add(
                    new ApiServiceInstanceProvider(UserRepository, ProductRepository, 
                        ConfigurationProvider, AuthenticationManager, AuthorizationManager));
            }
        }
    }
}