using System;

namespace WcfRestAuthentication.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute
    {
        private string _allowedRole;
        public AuthorizeAttribute(string allowedRole)
        {
            _allowedRole = allowedRole;
        }
    }
}