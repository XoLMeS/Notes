using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    class StaticRes
    {
        public static string USER_APP_DATA_DIR = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static ILogger LOGGER = new FileLogger();

        public static object DBContext { get; private set; }

        public static String HashPass(String password)
        {
            //https://stackoverflow.com/a/10402129/251311

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);
          
            return savedPasswordHash;
        }



        public static int minLoginLength = 3;
        public static int maxLoginLength = 16;
        public static int minPassLength = 8;
        public static int maxPassLength = 24;
    }
}
