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
    public partial class Lobby : PhoneApplicationPage
    {
        int tellerLobbyID;
        public Lobby()
        {
            InitializeComponent();
            GiveLobbyID(out tellerLobbyID);
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.GetAllSpelersCompleted += client_GetAllSpelersCompleted;
            client.GetAllSpelersAsync();
            client.GetAllLobbiesCompleted += client_GetAllLobbiesCompleted;
            client.GetAllLobbiesAsync();
        }

        void client_GetAllLobbiesCompleted(object sender, ServiceReference1.GetAllLobbiesCompletedEventArgs e)
        {
            LobbyClass showTijd = new LobbyClass();
            foreach (var item in e.Result)
            {
                if (item.ID == tellerLobbyID && item.AantalSpelers > 1)
                {
                    TijdBox.Text = "Game begins in " + item.Tijd + " sec...";
                    break;
                }
            }
        }

        void client_GetAllSpelersCompleted(object sender, ServiceReference1.GetAllSpelersCompletedEventArgs e)
        {            
            ObservableCollection<LobbyClass> showPlayers = new ObservableCollection<LobbyClass>();
            foreach (var item in e.Result)
            {
                
                if (item.LobbyID == tellerLobbyID)
                {
                    LobbyClass temp = new LobbyClass();
                    temp.SpelerNaam = item.NickName;
                    temp.Status = item.IsReady;

                    showPlayers.Add(temp);
                }               
            }
            mijnListbox.ItemsSource = showPlayers;
        }

        private static void GiveLobbyID(out int tellerLobbyID)
        {
            SpelerLokaal hulpLobby = new SpelerLokaal();
            string[] hulpString = new string[4];
            hulpString = hulpLobby.ReturnSpeler();
            tellerLobbyID = 0;
            do
            {
                if (hulpString[3].ToString() == tellerLobbyID.ToString())
                    break;
                else
                    tellerLobbyID++;
            } while (true);
            //tellerLobbyID--;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
        
    }
    public class LobbyClass
    {
        public string SpelerNaam { get; set; }
        public string Status { get; set; }
    }
}