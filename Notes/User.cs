using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    class User
    {

        private static int freeId_ = 0;
        private int id_;
        private String login_;
        private String password_;

        String name;
        String surname;
        String email;

        String lastLogin;

        public User(String name, String surname, String email, String login, String password)
        {
            id_ = freeId_;
            freeId_++;

            this.name = name;
            this.surname = surname;
            this.email = email;

            login_ = login;
            password_ = password;

        }

        public void UpdateLastLogin()
        {
            lastLogin = DateTime.Now.ToString("yyyy.MM.dd hh: mm:ss.ms");
        }

        public String GetLogin()
        {
            return this.login_;
        }

        public String GetPass()
        {
            return this.password_;
        }

        public int GetId()
        {
            return this.id_;
        }

    }
}
