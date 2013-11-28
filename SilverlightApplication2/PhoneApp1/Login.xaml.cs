using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PhoneApp1
{
    public partial class Login : PhoneApplicationPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        string username;
        string password;
        bool juisteLogin;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            username = Nickname.Text;
            password = PasswordBoxPassword.Password;
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.GetAllSpelersCompleted += client_GetAllSpelersCompleted;
            client.GetAllSpelersAsync();
        }

        void client_GetAllSpelersCompleted(object sender, ServiceReference1.GetAllSpelersCompletedEventArgs e)
        {
            juisteLogin = false;
            foreach (var item in e.Result)
            {
                if (item.NickName == username && item.Wachtwoord == password)
                {
                    juisteLogin = true;
                    break;
                } 
            }
            if (juisteLogin == true)
            {
                NavigationService.Navigate(new Uri("/GameBoard.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Foute Inlog");
 
            }
        }
    }
}