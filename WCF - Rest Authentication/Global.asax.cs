using System;
using System.ServiceModel;
using System.Web.Routing;
using System.ServiceModel.Activation;
using WcfRestAuthentication.Authentication;
using WcfRestAuthentication.Data;
using WcfRestAuthentication.Services.Api;

namespace WcfRestAuthentication
{
    public class Global : System.Web.HttpApplication
    {
        private IUserRepository UserRepository { get; set; }
        private IProductRepository ProductRepository { get; set; }
        private IConfigurationProvider ConfigurationProvider { get; set; }
        private ServiceAuthenticationManager AuthenticationManager { get; set; }
        private ServiceAuthorizationManager AuthorizationManager { get; set; }

        protected void Application_Start(object sender, EventArgs e)
        {
            ConfigureDependencies();
            MapRoutes(RouteTable.Routes);

        }

        private void ConfigureDependencies()
        {
            //ToDo: Push these into Castle Windsor
            UserRepository = new MyFakeUserRepository();
            ProductRepository = new MyFakeProductRepository();
            ConfigurationProvider = new MyConfigurationProvider();
            AuthenticationManager = new RestAuthenticationManager();
            AuthorizationManager = new RestAuthorizationManager();
        }

        private void MapRoutes(RouteCollection routes)
        {
            routes.Add(new ServiceRoute("api",
                new ApiServiceHostFactory(ConfigurationProvider, UserRepository, ProductRepository, AuthenticationManager, AuthorizationManager), 
                    typeof(ApiService)));
        }
    }
}