using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notes
{
    public partial class NoteForm : Form

    {
        private int noteId_;
        private MainWindow mainWindowInst;

        #region Constructor

        public NoteForm(int id, string title, string text, MainWindow mainW)
        {
            InitializeComponent();

            noteId_ = id;

            mainWindowInst = mainW;
            TitleBox.Text = title;
            TextBox.Text = text;

            SaveBtn.Enabled = false;
        }
        #endregion

        #region BtnEvents
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            MainWindow.SaveNote(noteId_, TitleBox.Text, TextBox.Text);
            mainWindowInst.lbNotes.Items.Refresh();
            Close();
        }
        #endregion

        #region TextBoxEvents
        private void TitleBox_TextChanged(object sender, EventArgs e)
        {
            SaveBtn.Enabled = true;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            SaveBtn.Enabled = true;
        }
        #endregion
    }
}
