using System;
using OpenB.Core.ACL;

namespace OpenB.Web
{
    public class SessionContext
    {
        private readonly User user;

        public SessionContext(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            this.user = user;
        }

        public User User { get; private set; }
    }
}