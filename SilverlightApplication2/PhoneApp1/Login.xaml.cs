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
        string username;
        string password;
        bool juisteLogin;

        public Login()
        {
            InitializeComponent();
        }
      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Login
            username = Nickname.Text; //Haal de username uit de textblox.
            password = PasswordBoxPassword.Password; //Haal het wachtwoord uit het passwordbox.
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.GetAllSpelersCompleted += client_GetAllSpelersCompleted; //Check als de login klopt.
            client.GetAllSpelersAsync();
        }
        void client_GetAllSpelersCompleted(object sender, ServiceReference1.GetAllSpelersCompletedEventArgs e)
        {
            juisteLogin = false;
            foreach (var item in e.Result)//Ga alle bestaande spelers af.
            {
                if (item.NickName == username && item.Wachtwoord == password) //Zie als de speler het juiste wachtwoord heeft ingegeven.
                {
                    juisteLogin = true;
                    //De lokale speler aanmaken. Deze gebruik ik voor op alle paginas te weten welke speler de lokale is.
                    SpelerLokaal temp = new SpelerLokaal();
                    temp.ID = item.ID;
                    temp.Nickname = item.NickName;
                    temp.Wachtwoord = item.Wachtwoord;
                    temp.LobbyID = -1;
                    break;
                } 
            }
            if (juisteLogin == true)
            {
                NavigationService.Navigate(new Uri("/Hoofdmenu.xaml", UriKind.Relative)); //Ga naar het hoofdmenu.
            }
            else
            {
                MessageBox.Show("Foute Inlog"); 
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }       
    }
}