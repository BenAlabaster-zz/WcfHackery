using System.ServiceModel;
using System.ServiceModel.Activation;
using WcfRestAuthentication.Authentication;
using WcfRestAuthentication.Data;

namespace WcfRestAuthentication.Services.Api
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public partial class ApiService
    {
        IUserRepository UserRepository { get; set; }
        IProductRepository ProductRepository { get; set; }
        IConfigurationProvider ConfigurationProvider { get; set; }

        #region Constructors & Destructor

        public ApiService(IUserRepository userRepository, IProductRepository productRepository,
            IConfigurationProvider configurationProvider)
        {
            UserRepository = userRepository;
            ProductRepository = productRepository;
            ConfigurationProvider = configurationProvider;
        }

        #endregion Constructors & Destructor
        
    }
}