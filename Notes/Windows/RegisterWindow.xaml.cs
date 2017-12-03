using System;
using System.Collections.Generic;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Notes.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {

        private Window parentWindow_;
       
        private readonly object _lock = new object();

        private bool _validName = false;
        private bool _validSurname = false;
        private bool _validEmail = false;
        private bool _validLogin = false;
        private bool _validPass = false;

        public RegisterWindow(Window parentWindow)
        {
            parentWindow_ = parentWindow;
          
            InitializeComponent();

            LoginTB.MaxLength = StaticRes.maxLoginLength;
            PassBox.MaxLength = StaticRes.maxPassLength;

            SignUp.IsEnabled = false;

        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            UserObj newUser = new UserObj(NameTB.Text, SurnameTB.Text, EmailTB.Text, LoginTB.Text, StaticRes.HashPass(PassBox.Password),1);
            Database.AddUser(newUser);

            parentWindow_.Show();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            parentWindow_.Show();
            this.Close();
        }

        private void LoginTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LoginTB.Text.Length >= StaticRes.minLoginLength)
            {
                CheckLogin();
            }
            else
            {
                _validLogin = false;
                LoginTB.Background = Brushes.Red;
            }
        }

        private void CheckLogin()
        {
            lock (_lock)
            {

                if (Database.UserExists(LoginTB.Text))
                {
                    _validLogin = false;
                    LoginTB.Background = Brushes.Red;
                }
                else
                {
                    _validLogin = true;
                    LoginTB.Background = Brushes.Green;
                }
            }

            CheckFields();
        }

        private void PassBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PassBox.Password.Length >= StaticRes.minPassLength)
            {
                _validPass = true;
                PassBox.Background = Brushes.Green;
            }
            else
            {
                _validPass = false;
                PassBox.Background = Brushes.Red;
            }

            CheckFields();
        }

        private void NameTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameTB.Text.Length >= 1)
            {
                _validName = true;
                NameTB.Background = Brushes.Green;
            }
            else
            {
                _validName = false;
                NameTB.Background = Brushes.Red;
            }

            CheckFields();
        }

        private void SurnameTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SurnameTB.Text.Length >= 1)
            {
                _validSurname = true;
                SurnameTB.Background = Brushes.Green;
            }
            else
            {
                _validSurname = false;
                SurnameTB.Background = Brushes.Red;
            }

            CheckFields();
        }

        private void EmailTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EmailTB.Text.Length >= 1)       //TO DO - BETTER VALIDATION
            {
                _validEmail = true;
                EmailTB.Background = Brushes.Green;
            }
            else
            {
                _validEmail = false;
                EmailTB.Background = Brushes.Red;
            }

            CheckFields();
        }

        private void CheckFields()
        {
            if (_validName && _validSurname && _validEmail && _validLogin && _validPass)
            {
                SignUp.IsEnabled = true;
            }
            else
            {
                SignUp.IsEnabled = false;
            }
        }

    }
}
