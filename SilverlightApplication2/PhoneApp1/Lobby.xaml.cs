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
        int tellerLobbyID;
        int tellerSpelerID = 0;
        int tijd;
        bool magIkTijdSetten = false;
        bool LetGameBegin = false;
        bool AllPlayersReady = false;
        // creating timer instance
        DispatcherTimer newTimer = new DispatcherTimer();
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

        void client_GetAllLobbiesCompleted(object sender, ServiceReference1.GetAllLobbiesCompletedEventArgs e)
        {
            LobbyClass showTijd = new LobbyClass();
            foreach (var item in e.Result)
            {
                if (item.ID == tellerLobbyID)
                {
                    TijdBox.Text = "Game begins in " + item.Tijd + " sec...";
                    tijd = item.Tijd;
                    if (tijd == 300)
                    {
                        magIkTijdSetten = true;
                    }
                    if (magIkTijdSetten == true && LetGameBegin == false)
                    {
                        tijd -= 1;
                        if (tijd == 0 && item.AantalSpelers > 1)
                        {
                            
                            LetGameBegin = true;
                            tijd = 300;
                            GiveLobbyID(out tellerLobbyID);
                            try
                            {
                                ServiceReference1.Service1Client client2 = new ServiceReference1.Service1Client();
                                client2.SetLetGameBeginCompleted += client2_SetLetGameBeginCompleted;
                                client2.SetLetGameBeginAsync(tellerLobbyID);
                            }
                            catch
                            {
                                MessageBox.Show("De SetLetGameBegin fout was er.");
                            }
                        }
                        else if (tijd == 0)
                        {
                            tijd = 300;
                        }
                        else if ((AllPlayersReady == true && tijd > 10 && item.AantalSpelers > 1) || (item.AantalSpelers == 4))
                        {
                            tijd = 10;
                        }
                        else
                        {
                            AllPlayersReady = false;
                        }
                        try
                        {
                            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                            client.SetTimeCompleted += client_SetTimeCompleted;
                            client.SetTimeAsync(tellerLobbyID, tijd);
                        }
                        catch
                        {
                            MessageBox.Show("De fout was er bij de SetTime.");
                        }

                    }                    
                    break;
                }
            }
        }

        void client2_SetLetGameBeginCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void client_SetTimeCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }
        void OnTimerTick(Object sender, EventArgs args)
        {
            // text box property is set to current system date.
            // ToString() converts the datetime value into text
            //txtClock.Text = DateTime.Now.ToString();
            UpdateLobby();

        }

        private void UpdateLobby()
        {
            GiveLobbyID(out tellerLobbyID);
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.GetAllSpelersCompleted += client_GetAllSpelersCompleted;
            client.GetAllSpelersAsync();
            client.GetAllLobbiesCompleted += client_GetAllLobbiesCompleted;
            client.GetAllLobbiesAsync();
            if (LetGameBegin == true || tijd == 1)
                NavigationService.Navigate(new Uri("/GameBoard.xaml", UriKind.Relative));
            if (tijd <= 10)
                LeaveButton.IsEnabled = false;
            else
                LeaveButton.IsEnabled = true;
        }

        void client_LetGameBeginCompleted(object sender, ServiceReference1.LetGameBeginCompletedEventArgs e)
        {
            if (e.Result == "Full")
            {
                //newTimer.Stop();
                LetGameBegin = true;
            }
        }
        void client_GetAllSpelersCompleted(object sender, ServiceReference1.GetAllSpelersCompletedEventArgs e)
        {            
            ObservableCollection<LobbyClass> showPlayers = new ObservableCollection<LobbyClass>();
            AllPlayersReady = true;
            foreach (var item in e.Result)
            {
                
                if (item.LobbyID == tellerLobbyID)
                {
                    LobbyClass temp = new LobbyClass();
                    temp.SpelerNaam = item.NickName;
                    temp.Status = item.IsReady;

                    showPlayers.Add(temp);
                    if (item.IsReady != "Ready")
                    {
                        AllPlayersReady = false;
                    }


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
        }
        private static void GiveSpelerID(ref int tellerSpelerID)
        {
            SpelerLokaal hulpSpeler = new SpelerLokaal();
            string[] hulpString = new string[4];
            hulpString = hulpSpeler.ReturnSpeler();
            do
            {
                if (hulpString[0].ToString() == tellerSpelerID.ToString())
                    break;
                else
                    tellerSpelerID++;
            } while (true);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GiveSpelerID(ref tellerSpelerID);
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.SetReadyCompleted += client_SetReadyCompleted;
            client.SetReadyAsync(tellerSpelerID);
            UpdateLobby();

        }

        void client_SetReadyCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GiveSpelerID(ref tellerSpelerID);
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.LeaveLobbyCompleted += client_LeaveLobbyCompleted;
            client.LeaveLobbyAsync(tellerLobbyID, tellerSpelerID);
            
        }

        void client_LeaveLobbyCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Hoofdmenu.xaml", UriKind.Relative));
        }
        
    }
    public class LobbyClass
    {
        public string SpelerNaam { get; set; }
        public string Status { get; set; }
    }
}