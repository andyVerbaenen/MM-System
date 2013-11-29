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
        public Hoofdmenu()
        {
            InitializeComponent();
            ShowLobbies();
        }

        void client_GetAllLobbiesCompleted(object sender, ServiceReference1.GetAllLobbiesCompletedEventArgs e)
        {
            ObservableCollection<LobbyClass> showLobbies = new ObservableCollection<LobbyClass>();
            foreach (var item in e.Result)
            {
                
                LobbyClass temp = new LobbyClass();
                temp.Lobby = "\tLobby " + item.ID;
                temp.AantalPlayers = item.AantalSpelers + "/4";
                temp.Status = item.Status + "";
                temp.Tijd = + item.Tijd + " sec";
                
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
    }

    public class LobbyClass
    {
        public string Lobby { get; set; }
        public string AantalPlayers { get; set; }
        public string Status { get; set; }
        public string Tijd { get; set; }
    }
}