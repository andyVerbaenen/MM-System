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
using System.Windows.Threading; 

namespace PhoneApp1
{
    public partial class Lobby : PhoneApplicationPage
    {
        #region Aanmaken variabelen
        int tellerLobbyID;
        int tellerSpelerID = 0;
        int tijd;
        bool magIkTijdSetten = false;
        bool LetGameBegin = false;
        bool AllPlayersReady = false;
        // creating timer instance
        DispatcherTimer newTimer = new DispatcherTimer();
        #endregion


        public Lobby()
        {
            InitializeComponent();
            UpdateLobby();
            
            // timer interval specified as 1 second
            newTimer.Interval = TimeSpan.FromSeconds(1);
            // Sub-routine OnTimerTick will be called at every 1 second
            newTimer.Tick += OnTimerTick;
            // starting the timer
            newTimer.Start();
            
            
        }


        #region Set - (HostID & Time & LetGameBegin)
        void client_SetHostIDCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }
        void client_SetTimeCompleted(object sender, ServiceReference1.SetTimeCompletedEventArgs e)
        {
            //if (e.Result.ToString() != "0" && e.Result.ToString() != "1")
            //{
            //    //MessageBox.Show(e.Result.ToString());
            //}
            
        }
        void client2_SetLetGameBeginCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }
        #endregion


        #region Update Lobby
        void OnTimerTick(Object sender, EventArgs args)
        {
            UpdateLobby();
        }
        private void UpdateLobby()
        {
            GiveLobbyID(out tellerLobbyID);
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.GetAllSpelersCompleted += client_GetAllSpelersCompleted; //Geef alle spelers in de lobby.
            client.GetAllSpelersAsync();
            client.GetAllLobbiesCompleted += client_GetAllLobbiesCompleted; //Geef de lobby weer om de gegevens er van te kunnen bepalen.
            client.GetAllLobbiesAsync();
            if (LetGameBegin == true || tijd == 1)
                NavigationService.Navigate(new Uri("/GameBoard.xaml", UriKind.Relative)); //Laat het spel beginnen.
            if (tijd <= 10)
                LeaveButton.IsEnabled = false; //Zorg er voor dat de spelers de lobby niet meer kunnen verlaten als iedereen heeft gezegd dat ze klaar zijn.
            else
                LeaveButton.IsEnabled = true;
        }
        #endregion


        #region LetGameBegin
        void client_LetGameBeginCompleted(object sender, ServiceReference1.LetGameBeginCompletedEventArgs e)
        {
            if (e.Result == "Full")
            {
                LetGameBegin = true;
            }
        }
        #endregion


        #region Get - (AllSpelers & AllLobbies)
        void client_GetAllSpelersCompleted(object sender, ServiceReference1.GetAllSpelersCompletedEventArgs e)
        {            
            ObservableCollection<LobbyClass> showPlayers = new ObservableCollection<LobbyClass>();
            AllPlayersReady = true;
            foreach (var item in e.Result)
            {               
                if (item.LobbyID == tellerLobbyID) //Geef alle spelers in de lobby.
                {
                    //Alle gegevens van de spelers tonen.
                    LobbyClass temp = new LobbyClass();
                    temp.SpelerNaam = item.NickName;
                    temp.Status = item.IsReady;

                    showPlayers.Add(temp); //Voeg alle spelers in de lobby toe aan de ObservableCollection.

                    if (item.IsReady != "Ready") //Wanneer een speler nog niet klaar is dit melden.
                    {
                        AllPlayersReady = false;
                    }
                }
            }
            mijnListbox.ItemsSource = showPlayers; //Update de spelers in de lobby.
        }
        void client_GetAllLobbiesCompleted(object sender, ServiceReference1.GetAllLobbiesCompletedEventArgs e)
        {
            LobbyClass showTijd = new LobbyClass();
            foreach (var item in e.Result)
            {
                if (item.ID == tellerLobbyID) //Geef de juiste lobby.
                {
                    TijdBox.Text = "Game begins in " + item.Tijd + " sec..."; //Laat de tijd op het scherm zien.
                    tijd = item.Tijd;
                    if (tijd == 300 && item.Status == "Waiting") //Voor te bepalen wie de tijd mag setten.
                    { //De speler die eerst joint, is de host. Deze code gebeurd 1 keer.
                        magIkTijdSetten = true;
                        GiveLobbyID(out tellerLobbyID);
                        GiveSpelerID(ref tellerSpelerID);
                        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                        client.SetHostIDCompleted += client_SetHostIDCompleted; //Stelt deze speler in als host player.
                        client.SetHostIDAsync(tellerSpelerID, tellerLobbyID);
                    }
                    if (magIkTijdSetten == true && LetGameBegin == false) //Wat de host player moet doen.
                    {
                        tijd -= 1; //Tel af..
                        if (tijd == 0 && item.AantalSpelers > 1) //Als de tijd op 0 staat en er zijn minstens twee spelers...
                        {
                            newTimer.Stop(); //Stop de timer.
                            LetGameBegin = true; //Laat het spel beginnen.
                            tijd = 400; 
                            GiveLobbyID(out tellerLobbyID);

                            ServiceReference1.Service1Client client2 = new ServiceReference1.Service1Client();
                            client2.SetLetGameBeginCompleted += client2_SetLetGameBeginCompleted; //Stel in dat het spel begint.
                            client2.SetLetGameBeginAsync(tellerLobbyID);

                        }
                        else if (tijd == 0) //Wanneer de tijd op 0 is en er zijn niet meer dan 2 spelers.
                        {
                            tijd = 120;
                        }
                        else if ((AllPlayersReady == true && tijd > 10 && item.AantalSpelers > 1))
                        {
                            tijd = 10; //Wanneer alle spelers klaar zijn het spel laten beignnen in 10 seconden.
                        }
                        else
                        {
                            AllPlayersReady = false; //Als er maar 1 speler is mag het spel niet beginnen.
                        }

                        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                        client.SetTimeCompleted += client_SetTimeCompleted; //Pas de tijd in de database aan.
                        client.SetTimeAsync(tellerLobbyID, tijd);

                    }
                    break;
                }
            }
        }
        #endregion

        #region Give - (LobbyID & SpelerID)
        private static void GiveLobbyID(out int tellerLobbyID)
        {
            SpelerLokaal hulpLobby = new SpelerLokaal();
            string[] hulpString = new string[4];
            hulpString = hulpLobby.ReturnSpeler();
            tellerLobbyID = 0;
            do //Via deze mannier omdat er geen ConvertToInt32() methode bestaat.
            {
                if (hulpString[3].ToString() == tellerLobbyID.ToString())
                    break;
                else
                    tellerLobbyID++;
            } while (true);
        }
        private static void GiveSpelerID(ref int tellerSpelerID)
        {
            SpelerLokaal hulpSpeler = new SpelerLokaal();
            string[] hulpString = new string[4];
            hulpString = hulpSpeler.ReturnSpeler();
            do //Via deze mannier omdat er geen ConvertToInt32() methode bestaat.
            {
                if (hulpString[0].ToString() == tellerSpelerID.ToString())
                    break;
                else
                    tellerSpelerID++;
            } while (true);
        }
        #endregion


        #region Button_Click
        private void Button_Click(object sender, RoutedEventArgs e) //Ik ben klaar voor te spelen.
        {
            GiveSpelerID(ref tellerSpelerID);
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.SetReadyCompleted += client_SetReadyCompleted; //Geef aan dat je klaar bent voor het spel.
            client.SetReadyAsync(tellerSpelerID);
            UpdateLobby();

        }
        private void Button_Click_1(object sender, RoutedEventArgs e) //Ik wil de lobby verlaten.
        {
            GiveSpelerID(ref tellerSpelerID);
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.LeaveLobbyCompleted += client_LeaveLobbyCompleted; //Verlaat de lobby.
            client.LeaveLobbyAsync(tellerLobbyID, tellerSpelerID);

        }
        #endregion

        #region SetReady & LeaveLobby
        void client_SetReadyCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }
        void client_LeaveLobbyCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Hoofdmenu.xaml", UriKind.Relative)); //Ga terug naar het hoofdmenu.
        }
        #endregion
    }
    public class LobbyClass //Hulp classe.
    {
        public string SpelerNaam { get; set; }
        public string Status { get; set; }
    }
}