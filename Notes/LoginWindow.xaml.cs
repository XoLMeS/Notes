using Notes.Windows;
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

namespace Notes
{
   
    public partial class LoginWindow : Window
    {

        private bool _validLogin = false;
        private bool _validPass = false;

        public LoginWindow()
        {
            InitializeComponent();

            Login.MaxLength = StaticRes.maxLoginLength;
            Pass.MaxLength = StaticRes.maxPassLength;

            LogBtn.IsEnabled = false;
        }


        #region BtnEvents
        private void BtnClickLog(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Database.CheckPass(Login.Text,Pass.Password))
                {
                    var userId = Database.GetId(Login.Text);
                    Database.UpdateLoginDate(userId);

                    MainWindow main = new MainWindow(userId);
                    main.Show();
                    StaticRes.LOGGER.Print("User #"+userId +" logged in");
                    Close();
                }
               
            }
            catch (UnauthorizedAccessException ex)
            {
                StaticRes.LOGGER.PrintError("LoginWindow.",ex);
                //TO DO. SHOW ALERT WINDOW
            }

        }

        private void BtnClickSign(object sender, RoutedEventArgs e)
        {
                RegisterWindow rw = new RegisterWindow(this);
                this.Hide();
                rw.Show();

        }
        #endregion

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Login.Text.Length >= StaticRes.minLoginLength)
            {
                _validLogin = true;
            }
            else
            {
                _validLogin = false;
            }
        }

        private void Pass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Pass.Password.Length >= StaticRes.minPassLength)
            {
                _validPass = true;
            }
            else
            {
                _validPass = false;
            }

            CheckFields();
        }

        private void CheckFields()
        {
            if (_validLogin && _validPass)
            {
                LogBtn.IsEnabled = true;
            }
            else
            {
                LogBtn.IsEnabled = false;
            }
        }
    }
}
