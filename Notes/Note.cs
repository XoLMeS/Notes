using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    class Note
    {   
        private static int _freeId = 0;
        private int _id;
        public string text_ { get; set; }
        public string title_ { get; set; }
        DateTime created_;
        DateTime lastEdited_;

        #region Constructor
        public Note(string title)
        {
            _id = _freeId;
            _freeId++;
            title_ = title;
            created_ = new DateTime();
            lastEdited_ = created_;
            
        }
#endregion

        public void Rename(string newTitle)
        {
            title_ = newTitle;
            lastEdited_ = new DateTime();
        }

        public void Edit(string text)
        {
            text_ = text;
            lastEdited_ = new DateTime();
        }

        public int GetId()
        {
            return _id;
        }


    }
}
