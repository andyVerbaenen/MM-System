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
        string username;
        string password;
        bool nieuweGebruikersnaam;
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

        public Register()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            username = Nickname.Text;
            password = PasswordBoxPassword.Password;
            if (PasswordBoxPassword.Password == PasswordBoxPassword2.Password) //Controleer als het paswoord het zelfde is.
            {
                client.GetAllSpelersCompleted += client_GetAllSpelersCompleted; //Controleer als speler al bestaat.
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
                if (username == item.NickName) //Check als speler al bestaat.
                {
                    nieuweGebruikersnaam = false;
                    break;
                }
            }
            if (nieuweGebruikersnaam == true) //Als hem nog niet bestaat.
            {
                client.AddSpelerCompleted += client_AddSpelerCompleted; //Maak nieuwe speler aan.
                client.AddSpelerAsync(username, password);
            }
            else //Hij bestaat al wel.
            {
                MessageBox.Show("Speler bestaat al.");
            }
        }

        void client_AddSpelerCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Speler aangemaakt.");
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative)); //Ga naar het login menu.
        }
    }
}