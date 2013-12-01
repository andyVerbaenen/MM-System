using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PhoneApp1
{
    public partial class Hoofdmenu : PhoneApplicationPage
    {
        int tellerLobbyID;
        int tellerSpelerID;

        public Hoofdmenu()
        {
            InitializeComponent();
            ShowLobbies();
        }

        void client_GetAllLobbiesCompleted(object sender, ServiceReference1.GetAllLobbiesCompletedEventArgs e)
        {
            ObservableCollection<HoofdmenuClass> showLobbies = new ObservableCollection<HoofdmenuClass>();
            foreach (var item in e.Result)
            {
                
                HoofdmenuClass temp = new HoofdmenuClass();
                temp.Lobby = "\tLobby " + item.ID;
                temp.AantalPlayers = item.AantalSpelers + "/4";
                temp.Status = item.Status + "";
                temp.Tijd = item.Tijd + " sec";
                temp.welkeLobby = item.ID.ToString();
                
                showLobbies.Add(temp);
            }
            mijnListbox.ItemsSource = showLobbies;
            
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.AddLobbyCompleted += client_AddLobbyCompleted;
            client.AddLobbyAsync();
        }

        void client_AddLobbyCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            ShowLobbies();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            //ShowLobbies();
        }
        public void ShowLobbies()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.GetAllLobbiesCompleted += client_GetAllLobbiesCompleted;
            client.GetAllLobbiesAsync();
        }

        private void mijnListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        private void Button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
            GiveSpelerAndLobbyID(sender, out tellerLobbyID, out tellerSpelerID);

            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.JoinLobbyCompleted += client_JoinLobbyCompleted;
            client.JoinLobbyAsync(tellerLobbyID, tellerSpelerID);
        }

        private static void GiveSpelerAndLobbyID(object sender, out int tellerLobbyID, out int tellerSpelerID)
        {
            Button hulp = sender as Button;
            tellerLobbyID = 0;
            do
            {
                
                if (tellerLobbyID.ToString() == hulp.Tag.ToString())
                    break;
                else
                    tellerLobbyID++;

            } while (true);
            //MessageBox.Show(hulp.Tag.ToString() + "  " + tellerLobbyID);
            SpelerLokaal hulpSpeler = new SpelerLokaal();
            string[] hulpString = new string[4];
            hulpString = hulpSpeler.ReturnSpeler();
            hulpSpeler.LobbyID = tellerLobbyID;
            tellerSpelerID = 0;
            do
            {
                if (hulpString[0].ToString() == tellerSpelerID.ToString())
                    break;
                else
                    tellerSpelerID++;
            } while (true);
        }

        void client_JoinLobbyCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

            NavigationService.Navigate(new Uri("/Lobby.xaml", UriKind.Relative));
            
        }
    }

    public class HoofdmenuClass
    {
        public string Lobby { get; set; }
        public string AantalPlayers { get; set; }
        public string Status { get; set; }
        public string Tijd { get; set; }
        public string welkeLobby { get; set; }
    }
}