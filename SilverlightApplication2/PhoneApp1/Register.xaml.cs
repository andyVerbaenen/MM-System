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
    public partial class Register : PhoneApplicationPage
    {
        public Register()
        {
            InitializeComponent();
        }
        string username;
        string password;
        bool nieuweGebruikersnaam;
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            username = Nickname.Text;
            password = PasswordBoxPassword.Password;
            if (PasswordBoxPassword.Password == PasswordBoxPassword2.Password)
            {
                client.GetAllSpelersCompleted += client_GetAllSpelersCompleted;
                client.GetAllSpelersAsync();
            }
            else
            {
                MessageBox.Show("2 Verschillende paswoorden ingegeven.");
            }
        }

        void client_GetAllSpelersCompleted(object sender, ServiceReference1.GetAllSpelersCompletedEventArgs e)
        {
            nieuweGebruikersnaam = true;
            foreach (var item in e.Result)
            {
                if (username == item.NickName)
                {
                    nieuweGebruikersnaam = false;
                    break;
                }
            }
            if (nieuweGebruikersnaam == true)
            {
                client.AddSpelerCompleted += client_AddSpelerCompleted;
                client.AddSpelerAsync(username, password);
            }
            else
            {
                MessageBox.Show("Speler bestaat al.");
            }
        }

        void client_AddSpelerCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Speler aangemaakt.");
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }
    }
}