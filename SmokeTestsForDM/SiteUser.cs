using System;

namespace RedGate.Deploy.WebAppTests.Utilities
{
    public struct SiteUser
    {
        public SiteUser(string userName, string password) : this()
        {
            UserName = userName;
            Password = password;
        }

        public readonly string UserName;
        public readonly string Password;

        public override string ToString()
        {
            return UserName;
        }

        public static readonly SiteUser DefaultAdminUser = new SiteUser("AdminUser", "Password");
    }
}
