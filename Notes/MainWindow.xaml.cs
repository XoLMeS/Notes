using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notes
{

    public partial class MainWindow : Window
    {

        static List<Note> items = new List<Note>();

        public MainWindow()
        {
            InitializeComponent();
            lbNotes.ItemsSource = items;
        }

        #region BtnEvents
        private void BtnCreate(object sender, RoutedEventArgs e)
        {
            Note newNote = new Note("New note");
            items.Add(newNote);
            NoteForm form = new NoteForm(newNote.GetId(), newNote.title_,"",this);
            form.Show();
            Console.WriteLine("Created");
          
            lbNotes.Items.Refresh();
        }

        private void BtnEdit(object sender, RoutedEventArgs e)
        {
            if (lbNotes.SelectedItem != null) { 
                Note selectedNote = (lbNotes.SelectedItem as Note);
                NoteForm form = new NoteForm(selectedNote.GetId(), selectedNote.title_, selectedNote.text_,this);
                form.Show();
                Console.WriteLine("Edited");
             }
        }

        private void BtnDelete(object sender, RoutedEventArgs e)
        {
            if (lbNotes.SelectedItem != null)
            {
                Note selectedNote = (lbNotes.SelectedItem as Note);
               
                for (int i = 0; i < items.Count; i++)
                {
                    if (items.ElementAt(i).GetId()==selectedNote.GetId())
                    {
                        items.RemoveAt(i);
                    }
                  
                }
                lbNotes.Items.Refresh();
                Console.WriteLine($"Note id#{selectedNote.GetId()} deleted ");
            }
        }
#endregion

        public static void SaveNote(int id, string title, string text)
        {
            items.ForEach(delegate (Note n)
            {
                if (n.GetId()==id)
                {
                    n.text_ = text;
                    n.title_ = title;
                }
            });
        }

    }
}
