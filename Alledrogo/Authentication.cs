using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DatabaseAccess;

namespace Alledrogo
{
    public class Authentication
    {
        public static bool Login(string username , string password)
        {
            using(DatabaseEntities entities = new DatabaseEntities())
            {
                return entities.Users.Any(user => user.Email.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password);
            }
        }
    }
}