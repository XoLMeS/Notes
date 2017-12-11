using Notes.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;


namespace Notes
{


    public partial class MainWindow : Window
    {
        BackgroundWorker worker = new BackgroundWorker();

        List<NoteObj> items;
        

        internal static UserObj currentUser;

        public MainWindow(UserObj user)
        {
            currentUser = user;
            StaticRes.LOGGER.Print(""+currentUser.UserId);
            items = Database.getUserNotes(currentUser.UserId);
            InitializeComponent();
            lbNotes.ItemsSource = items;

            Closing += (this.OnApplicationExit);

            ProgBar.IsIndeterminate = false;
            HideProgressBar();
        }

 #region BtnEvents
        private void BtnCreate(object sender, RoutedEventArgs e)
        {
            NoteObj newNote = new NoteObj("New note");
            items.Add(newNote);
            Database.CreateNote(newNote);
            NoteWindow form = new NoteWindow(newNote.NoteId, newNote.Title,"",this);
            form.Show();
            lbNotes.Items.Refresh();
            StaticRes.LOGGER.Print(currentUser.UserId +" created Note #"+newNote.NoteId);
        }

        private void BtnEdit(object sender, RoutedEventArgs e)
        {
            if (lbNotes.SelectedItem != null) {
                NoteObj selectedNote = (lbNotes.SelectedItem as NoteObj);
                NoteWindow form = new NoteWindow(selectedNote.NoteId, selectedNote.Title, selectedNote.Content,this);
                form.Show();
            }
        }

        private void BtnDelete(object sender, RoutedEventArgs e)
        {
            if (lbNotes.SelectedItem != null)
            {
                NoteObj selectedNote = (lbNotes.SelectedItem as NoteObj);
               
                for (int i = 0; i < items.Count; i++)
                {
                    if (items.ElementAt(i).NoteId==selectedNote.NoteId)
                    {
                        Database.DeleteNote(items.ElementAt(i));
                        items.RemoveAt(i);
                        StaticRes.LOGGER.Print(currentUser.UserId + " deleted Note #" + selectedNote.NoteId);
                        break;
                    }
                  
                }
                lbNotes.Items.Refresh();
            }
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lg = new LoginWindow();
            lg.Show();
            StaticRes.LOGGER.Print("User #" + currentUser.UserId + " logged out");
            this.Close();
        }
        #endregion

        public void SaveNote(int id, string title, string text)
        {
            items.ForEach(delegate (NoteObj n)
            {
            if (n.NoteId == id)
                {
                n.Content = text;
                n.Title = title;
                StaticRes.LOGGER.Print(MainWindow.currentUser.UserId + " edited Note #" + id);
                   
                    worker.WorkerReportsProgress = true;
                    worker.DoWork += Worker_DoWork;
                    worker.ProgressChanged += Worker_ProgressChanged;
                    worker.RunWorkerAsync();
                    ShowProgressBar();

                    Thread newThread = new Thread(DoWork);
                    newThread.Start(new NoteObj(title, text, id, currentUser.UserId));
                }
            });
        }

        public static void DoWork(object data)
        {
            Console.WriteLine("Saving...");
            Database.UpdateNote((NoteObj) data);
        }

        void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(10);
            }
            HideProgressBar();
        }

        void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgBar.Value = e.ProgressPercentage;
        }


        private void OnApplicationExit(object sender, EventArgs e)
        {
            SerializeManager.Serialize(currentUser);
            StaticRes.LOGGER.Print(currentUser.UserId + "Serializzed");
            worker.Dispose();
        }

        private void HideProgressBar()
        {
            this.Dispatcher.Invoke((Action)(() => {
                ProgBar.Visibility = Visibility.Hidden;
                LabelSaving.Visibility = Visibility.Hidden;
            }));
        }
        private void ShowProgressBar()
        {
            this.Dispatcher.Invoke((Action)(() => {
                ProgBar.Visibility = Visibility.Visible;
                LabelSaving.Visibility = Visibility.Visible;
            }));
        }

        
    }
}
