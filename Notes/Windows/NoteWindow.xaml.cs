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
using System.Windows.Shapes;

namespace Notes.Windows
{
    /// <summary>
    /// Логика взаимодействия для NoteWindow.xaml
    /// </summary>
    public partial class NoteWindow : Window
    {

        private int noteId_;
        private MainWindow mainWindowInst;

        #region Constructor
        public NoteWindow(int id, string title, string text, MainWindow mainW)
        {
            InitializeComponent();

            noteId_ = id;

            mainWindowInst = mainW;
            TBTitle.Text = title;
            TBText.Text = text;

            BtnSave.IsEnabled = false;
        }
        #endregion


        #region BtnEvents
        private void Button_Click_BtnCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_BtnSave(object sender, RoutedEventArgs e)
        {
            mainWindowInst.SaveNote(noteId_, TBTitle.Text, TBText.Text);
            mainWindowInst.lbNotes.Items.Refresh();
            Close();
        }

        #endregion

        #region TextBoxEvents
        private void TBText_TextChanged(object sender, TextChangedEventArgs e)
        {
            BtnSave.IsEnabled = true;
        }

        private void TBTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            BtnSave.IsEnabled = true;
        }
        #endregion

    }
}
