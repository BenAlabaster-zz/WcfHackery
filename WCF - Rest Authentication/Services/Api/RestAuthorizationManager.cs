using System;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.ServiceModel;
using System.Threading;
using System.Web.Security;

namespace WcfRestAuthentication.Services.Api
{
    public class RestAuthorizationManager : ServiceAuthorizationManager
    {
        //CheckAccessCore should be used to validate system access 
        //  i.e. is the user account active? is it locked out?
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            var principal = Thread.CurrentPrincipal;

            //ToDo: Wire up actual authorization evaluation
            if (principal == null)
                throw new System.ServiceModel.Security.SecurityAccessDeniedException("Computah says no!");

            return base.CheckAccessCore(operationContext);
        }
    }

    public class RestAuthorizationPolicy : IAuthorizationPolicy
    {
        //Evaluate should be used to validate operation authorization
        // i.e. This user has been authenticated, the account is active and 
        //      has valid authorization to access the system
        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            //ToDo: Wire up actual authorization evaluation
            return true;
        }

        public System.IdentityModel.Claims.ClaimSet Issuer
        {
            get { return ClaimSet.System; }
        }

        public string Id
        {
            get { return "RestAuthorizationPolicy"; }
        }
    }
}