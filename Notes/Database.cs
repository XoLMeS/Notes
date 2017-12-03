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

        public static bool UserExists(String login)
        {
            using (var db = new TestEntities3())
            {
                var query = from b in db.Users
                            orderby b.Login
                            select b;

                foreach (var item in query)
                {
                    if (item.Login == login)
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        public static bool CheckPass(String login, String pass)
        {
            bool userExists = false;
            byte[] hashBytes = null;

            using (var db = new TestEntities3())
            {
                var query = from b in db.Users
                            orderby b.Login
                            select b;

                foreach (var item in query)
                {
                    if (item.Login == login)
                    {
                        userExists = true;
                        hashBytes = Convert.FromBase64String(item.Password);
                        break;
                    }

                }
            }

            if (userExists)
            {
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

                return true;
            }

            throw new UnauthorizedAccessException("Database. Inccorect password");
           
        }

        public static int GetId(String login)
        {
            using (var db = new TestEntities3())
            {
                var query = from b in db.Users
                            orderby b.UserId
                            select b;

                foreach (var item in query)
                {
                    if (item.Login == login)
                    {
                        return item.UserId;
                    }

                }
            }
            return -1;
        }

        public static void UpdateLoginDate(int userId)
        {
            using (var db = new TestEntities3())
            {
                var query = from b in db.Users
                            orderby b.UserId
                            select b;

                foreach (var item in query)
                {
                    if (item.UserId == userId)
                    {
                        item.LastLogin = DateTime.Now;
                        break;
                    }

                }
                db.SaveChanges();
            }
        }
        
        public static UserObj GetUser(int id)
        {
            using (var db = new TestEntities3())
            {
                var query = from b in db.Users
                            orderby b.UserId
                            select b;

                foreach (var item in query)
                {
                    if (item.UserId == id)
                    {
                        return new UserObj(item.Name, item.Surname,item.Email,  item.Login, item.Password,item.UserId);
                    }
                   
                }
            }
            return null;
        }

        public static bool AddUser(UserObj uo)
        {
            try
            {
                using (var db = new TestEntities3())
                {
                    var user = new User { Name = uo.Name, Surname = uo.Surname, Email = uo.Email, Login = uo.Login, Password = uo.Password };
                    db.Users.Add(user);
                    db.SaveChanges();
                    StaticRes.LOGGER.Print("Database. New user #" + uo.UserId + " registreted.");
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<NoteObj> getUserNotes(int id)
        {
            List<NoteObj> notes = new List<NoteObj>();
            using (var db = new TestEntities3())
            {
                var query = from b in db.Notes
                            orderby b.UserId
                            select b;

                foreach (var item in query)
                {
                    if (item.UserId == id)
                    {
                        notes.Add(new NoteObj(item.Title,item.Content,item.NoteId,item.UserId));
                        NoteObj.freeNoteId = item.NoteId++;
                    }

                }
            }
            return notes;
        }

        public static bool UpdateNote(NoteObj no)
        {
            
            using (var db = new TestEntities3())
            {
                try
                {
                    var query = from b in db.Notes
                                orderby b.NoteId
                                select b;

                    foreach (var item in query)
                    {
                        if (item.NoteId == no.NoteId)
                        {
                            item.Content = no.Content;
                            item.Title = no.Title;
                            StaticRes.LOGGER.Print("Database. Note Updated #" + no.NoteId + ".");
                            break;
                        }

                    }
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
               
        }

        public static bool CreateNote(NoteObj no)
        {
            using (var db = new TestEntities3())
            {
                try
                {
                    var note = new Note { Title = no.Title, Content = no.Content, NoteId = no.NoteId, UserId = no.UserId };
                    db.Notes.Add(note);
                    db.SaveChanges();
                    StaticRes.LOGGER.Print("Database. Note Created #" + no.NoteId + ".");
                    return true;
                }
                catch
                {
                    return false;
                }
            }
             
        }

        public static bool DeleteNote(NoteObj no)
        {
            using (var db = new TestEntities3())
            {
                try
                {
                    var query = from b in db.Notes
                                orderby b.NoteId
                                select b; 

                    foreach (var item in query)
                    {
                        if (item.NoteId == no.NoteId)
                        {
                            db.Notes.Remove(item);
                            StaticRes.LOGGER.Print("Database. Note Deleted #" + no.NoteId + ".");
                            break;
                        }

                    }
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
             
        }


    }
}
