using System;
using System.Linq;
using WcfRestAuthentication.Model;

namespace WcfRestAuthentication.Data
{
    public class MyFakeUserRepository : IUserRepository
    {
        public Model.User Get(System.Guid userId)
        {
            return User.Create(userId, "TestAdmin", "Test", "Administrator", new String[] {"Administrator"});
        }

        public IQueryable<Model.User> Query(Func<Model.User, bool> expression)
        {
            return new[]
            {
                User.Create(Guid.NewGuid(), "TestAdmin", "Test", "Administrator", new[] { "Administrator" }),
                User.Create(Guid.NewGuid(), "TestUser", "Test", "User", new[] { "User" })
            }.AsQueryable();
        }

        public Model.User Update(Model.User user)
        {
            return user;
        }

        public void Delete(System.Guid userId)
        {
        }

    }
}