using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    [Serializable]
    public class UserObj : ISerializable
    {
        public UserObj(String name, String surname, String  email, String login, String pass,int userId)
        {
            UserId = userId;
            Name = name;
            Surname = surname;
            Email = email;
            Login = login;
            Password = pass;
        }

        public void UpdateLastLogin()
        {
            LastLogin = DateTime.Now;
        }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }

        public string Filename => "UserObj";
    }
}
