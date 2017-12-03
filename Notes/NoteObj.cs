using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    class NoteObj
    {

        public static int freeNoteId = 0;

        public NoteObj(String title)
        {
            Title = title;
            NoteId = freeNoteId;
            freeNoteId++;
        }

        public NoteObj(String title,String content,int id,int userId)
        {
            Title = title;
            Content = content;
            NoteId = id;
            UserId = userId;
        }

        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}
