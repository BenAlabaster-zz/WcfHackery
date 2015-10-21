using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using WcfRestAuthentication.Extensions;

namespace WcfRestAuthentication.Services.Api.Endpoints.User.V1.Behaviors
{
    public class UserEndpointWebHttpBehavior : WebHttpBehavior
    {
        private ServiceAuthenticationManager AuthenticationManager { get; set; }
        private ServiceAuthorizationManager AuthorizationManager { get; set; }

        private IEnumerable<IDispatchMessageInspector> _messageInspectors { get; set; }

        public UserEndpointWebHttpBehavior(ServiceAuthenticationManager authenticationManager, ServiceAuthorizationManager authorizationManager) 
            : this()
        {
            AuthenticationManager = authenticationManager;
            AuthorizationManager = authorizationManager;
        }

        public UserEndpointWebHttpBehavior()
            : this(new IDispatchMessageInspector[]{ })
        { }

        public UserEndpointWebHttpBehavior(IDispatchMessageInspector messageInspector)
            : this(new [] { messageInspector })
        { }

        public UserEndpointWebHttpBehavior(IEnumerable<IDispatchMessageInspector> messageInspectors)
        {
            _messageInspectors = messageInspectors;
        }

        public override void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            if (_messageInspectors.Any())
                endpointDispatcher.AddMessageInspectors(_messageInspectors);

            endpointDispatcher.DispatchRuntime.ServiceAuthenticationManager = AuthenticationManager;
            endpointDispatcher.DispatchRuntime.ServiceAuthorizationManager = AuthorizationManager;

            base.ApplyDispatchBehavior(endpoint, endpointDispatcher);
        }
    }
}