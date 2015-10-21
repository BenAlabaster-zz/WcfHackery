using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using WcfRestAuthentication.Authentication;
using WcfRestAuthentication.Data;
using WcfRestAuthentication.Extensions;

namespace WcfRestAuthentication.Services.Api.Endpoints.Product.V1.Behaviors
{
    public class ProductEndpointWebHttpBehavior : WebHttpBehavior
    {
        private ServiceAuthenticationManager AuthenticationManager { get; set; }
        private ServiceAuthorizationManager AuthorizationManager { get; set; }

        public override WebMessageFormat DefaultOutgoingRequestFormat
        {
            get { return defaultOutgoingRequestFormat; }
            set { defaultOutgoingRequestFormat = value; }
        }
        private WebMessageFormat defaultOutgoingRequestFormat;

        public override WebMessageFormat DefaultOutgoingResponseFormat
        {
            get { return defaultOutgoingResponseFormat; }
            set { defaultOutgoingResponseFormat = value; }
        }
        private WebMessageFormat defaultOutgoingResponseFormat;

        private IEnumerable<IDispatchMessageInspector> _messageInspectors { get; set; }

        private const WebMessageFormat DefaultMessageFormat = WebMessageFormat.Json;

        #region Constructors & Destructor

        public ProductEndpointWebHttpBehavior(ServiceAuthenticationManager authenticationManager, ServiceAuthorizationManager authorizationManager) 
            : this()
        {
            AuthenticationManager = authenticationManager;
            AuthorizationManager = authorizationManager;
        }

        public ProductEndpointWebHttpBehavior() 
            : this(new List<IDispatchMessageInspector>() { }, DefaultMessageFormat, DefaultMessageFormat)
        {
        }

        public ProductEndpointWebHttpBehavior(IDispatchMessageInspector messageInspector)
            : this(new[] { messageInspector }, DefaultMessageFormat, DefaultMessageFormat)
        {
        }

        public ProductEndpointWebHttpBehavior(IEnumerable<IDispatchMessageInspector> messageInspectors, 
            WebMessageFormat outgoingRequestFormat, WebMessageFormat outgoingResponseFormat)
        {
            _messageInspectors = messageInspectors;
            this.defaultOutgoingRequestFormat = outgoingRequestFormat;
            this.defaultOutgoingResponseFormat = outgoingResponseFormat;
        }

        #endregion Constructors

        #region WebHttpBehavior

        public override void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            if (_messageInspectors.Any())
                endpointDispatcher.AddMessageInspectors(_messageInspectors);

            //Assign our customer ServiceAuthenticationManager that accepts an authentication provider 
            //that can be hooked into our user repository...
            endpointDispatcher.DispatchRuntime.ServiceAuthenticationManager = AuthenticationManager;
            //Authorization manager doesn't require any injection because it utilizes roles based on 
            //the CurrentPrincipal. If it needed any parameters passed in, you would add it here using
            endpointDispatcher.DispatchRuntime.ServiceAuthorizationManager = AuthorizationManager;

            base.ApplyDispatchBehavior(endpoint, endpointDispatcher);
        }

        public override void Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion WebHttpBehavior
    }
}