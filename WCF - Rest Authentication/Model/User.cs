using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WcfRestAuthentication.Model
{
    [DataContract]
    public class User : IEntity<Guid>
    {
        [DataMember]
        public Guid Id { get; protected set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public IEnumerable<string> Roles { get; protected set; }

        public static User Create(Guid id, string userName, string firstName, string lastName, IEnumerable<string> roles)
        {
            return new User()
            {
                Id = id,
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                Roles = roles
            };
        }

        public User()
        {
            Roles = new List<string>().AsReadOnly();
        }
    }
}