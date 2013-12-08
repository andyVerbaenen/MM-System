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

        #region (Get & Add & Join) - Lobby Completed
        void client_GetAllLobbiesCompleted(object sender, ServiceReference1.GetAllLobbiesCompletedEventArgs e)
        {
            ObservableCollection<HoofdmenuClass> showLobbies = new ObservableCollection<HoofdmenuClass>();
            foreach (var item in e.Result)
            {
                //Get alle gegevens.
                HoofdmenuClass temp = new HoofdmenuClass();
                temp.Lobby = "\tLobby " + item.ID;
                temp.AantalPlayers = item.AantalSpelers + "/4";
                temp.Status = item.Status + "";
                temp.Tijd = item.Tijd + " sec";
                temp.welkeLobby = item.ID.ToString();

                //Voeg toe in ObservableCollection.
                showLobbies.Add(temp);
            }
            mijnListbox.ItemsSource = showLobbies; //Laat alle lobbies zijn in het lobby menu.
            
        }
        void client_AddLobbyCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            ShowLobbies(); //Show alle lobbies
        }
        void client_JoinLobbyCompleted(object sender, ServiceReference1.JoinLobbyCompletedEventArgs e)
        {
            if (e.Result == "Waiting")
                NavigationService.Navigate(new Uri("/Lobby.xaml", UriKind.Relative)); //Ga naar de lobby.
        }
        #endregion

        #region Buttons_Click
        private void Button_Click(object sender, RoutedEventArgs e) //Maak lobby.
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.AddLobbyCompleted += client_AddLobbyCompleted; //Voeg nieuwe lobby toe.
            client.AddLobbyAsync();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e) //Refresche 
        {           
            ShowLobbies(); //Laat de huidige stand van zaken zien.
        }
        private void Button_Click_2(object sender, RoutedEventArgs e) //Join lobby
        {
            
            GiveSpelerAndLobbyID(sender, out tellerLobbyID, out tellerSpelerID); //Spreekt voor zich...

            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.JoinLobbyCompleted += client_JoinLobbyCompleted; //Join lobby
            client.JoinLobbyAsync(tellerLobbyID, tellerSpelerID);
        }
        #endregion

        #region ShowLobbies & GiveSpelerAndLobbyID
        public void ShowLobbies()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.GetAllLobbiesCompleted += client_GetAllLobbiesCompleted; //Toen alle lobbys op het scherm.
            client.GetAllLobbiesAsync();
        }
        private static void GiveSpelerAndLobbyID(object sender, out int tellerLobbyID, out int tellerSpelerID)
        {
            Button hulp = sender as Button;
            tellerLobbyID = 0;
            do //Bepaal de ID van het lobby waar we in spelen.
            {
                if (tellerLobbyID.ToString() == hulp.Tag.ToString()) //Omdat ik de ConvertToInt32() methode niet kan gebruiken.
                    break;
                else
                    tellerLobbyID++;
            } while (true);

            SpelerLokaal hulpSpeler = new SpelerLokaal();
            string[] hulpString = new string[4];
            hulpString = hulpSpeler.ReturnSpeler();
            hulpSpeler.LobbyID = tellerLobbyID;
            tellerSpelerID = 0;
            do //Bepaal het ID van de huidige speler.
            {
                if (hulpString[0].ToString() == tellerSpelerID.ToString()) //Omdat ik de ConvertToInt32() methode niet kan gebruiken.
                    break;
                else
                    tellerSpelerID++;
            } while (true);
        }
        #endregion

        #region Niet gebruikte events
        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void mijnListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }
        private void Button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
        #endregion
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