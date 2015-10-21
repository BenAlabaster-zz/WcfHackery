using System;
using System.Linq;
using WcfRestAuthentication.Model;

namespace WcfRestAuthentication.Data
{
    public interface IUserRepository
    {
        User Get(Guid userId);

        User Update(User user);

        void Delete(Guid userId);

        IQueryable<User> Query(Func<User, bool> expression);
    }
}