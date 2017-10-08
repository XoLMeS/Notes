using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    class Database
    {

        private static List<User> users = new List<User>();

        public static bool UserExists(String login)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users.ElementAt(i).GetLogin().Equals(login))
                {
                    return true;
                }
            }
             
            return false;
        }

        public static bool CheckPass(String login, String pass)
        {
            bool userExists = false;
            for (int j = 0; j < users.Count; j++)
            {
                if (users.ElementAt(j).GetLogin().Equals(login))
                {
                    userExists = true;
                    byte[] hashBytes = Convert.FromBase64String(users.ElementAt(j).GetPass());
                    /* Get the salt */
                    byte[] salt = new byte[16];
                    Array.Copy(hashBytes, 0, salt, 0, 16);
                    /* Compute the hash on the password the user entered */
                    var pbkdf2 = new Rfc2898DeriveBytes(pass, salt, 10000);
                    byte[] hash = pbkdf2.GetBytes(20);
                    /* Compare the results */
                    for (int i = 0; i < 20; i++)
                    {
                        if (hashBytes[i + 16] != hash[i])
                            throw new UnauthorizedAccessException("Database. Inccorect login");
                    }
                    break;
                }
            }

            if (userExists)
            {
                return true;
            }
            else
            {
                throw new UnauthorizedAccessException("Database. Inccorect password");
            }
        }

        public static int GetId(String login)
        {
            for (int j = 0; j < users.Count; j++)
            {
                if (users.ElementAt(j).GetLogin().Equals(login))
                {
                    return j;
                }

            }
            return -1;
        }


        public static void AddUser(User newUser)
        {
            users.Add(newUser);
            StaticRes.LOGGER.Print("Database. New user #"+ newUser.GetId() + " registreted.");
        }

        public static void UpdateLoginDate(int userId)
        {
            for (int j = 0; j < users.Count; j++)
            {
                if (users.ElementAt(j).GetId() == userId)
                {
                    users.ElementAt(j).UpdateLastLogin();
                }

            }
        }
     
    }
}
