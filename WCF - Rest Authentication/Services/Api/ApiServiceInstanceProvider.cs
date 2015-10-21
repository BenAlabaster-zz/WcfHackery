using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using WcfRestAuthentication.Authentication;
using WcfRestAuthentication.Data;
using WcfRestAuthentication.Services.Api.Endpoints.Product;
using WcfRestAuthentication.Services.Api.Endpoints.Product.V1.Behaviors;
using WcfRestAuthentication.Services.Api.Endpoints.User;
using WcfRestAuthentication.Services.Api.Endpoints.User.V1.Behaviors;

namespace WcfRestAuthentication.Services.Api
{
    public class ApiServiceInstanceProvider : IInstanceProvider, IContractBehavior
    {
        private IUserRepository UserRepository { get; set; }
        private IProductRepository ProductRepository { get; set; }
        private IConfigurationProvider ConfigurationProvider { get; set; }

        private ServiceAuthenticationManager AuthenticationManager { get; set; }

        private ServiceAuthorizationManager AuthorizationManager { get; set; }

        private ApiService ApiService { get; set; }

        public ApiServiceInstanceProvider(IUserRepository userRepository, IProductRepository productRepository,
            IConfigurationProvider configurationProvider, ServiceAuthenticationManager authenticationManager, ServiceAuthorizationManager authorizationManager)
        {
            UserRepository = userRepository;
            ProductRepository = productRepository;
            ConfigurationProvider = configurationProvider;
            AuthenticationManager = authenticationManager;
            AuthorizationManager = authorizationManager;

            ApiService = new ApiService(UserRepository, ProductRepository, ConfigurationProvider);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return ApiService;
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return ApiService;
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
        }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceProvider = this;
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }
    }
}