using System;
using WcfRestAuthentication.Model;
using WcfRestAuthentication.Services.Api.Endpoints.User;

namespace WcfRestAuthentication.Services.Api
{
    public partial class ApiService : IUserService
    {

        #region IUserService

        public User Get(Guid userId)
        {
            return UserRepository.Get(userId);
        }

        public User Post(User user)
        {
            return UserRepository.Update(user);
        }

        public User Put(User user)
        {
            return UserRepository.Update(user);
        }

        public void DeleteUser(Guid userId)
        {
            UserRepository.Delete(userId);
        }

        #endregion IUserService
    }
}