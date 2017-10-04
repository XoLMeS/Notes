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
        public LoginWindow()
        {
            InitializeComponent();
        }


        #region BtnEvents
        private void BtnClickLog(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Close();
            main.Show();
        }

        private void BtnClickSign(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
