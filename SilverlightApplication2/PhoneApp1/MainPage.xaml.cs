using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp1.Resources;

namespace PhoneApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            //ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            //client.GetAllSpelersCompleted += client_GetAllSpelersCompleted;
            //client.GetAllSpelersAsync();
        }

        void client_GetAllSpelersCompleted(object sender, ServiceReference1.GetAllSpelersCompletedEventArgs e)//Test methode.
        {
            string showResult = "De Spelers:";

            foreach (var item in e.Result)
            {
                showResult += "\nSpeler: " + item.NickName;
            }
            MessageBox.Show(showResult);
        }

        private void Button_Click(object sender, RoutedEventArgs e) //Voor het inloggen.
        {
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative)); 
        }
        private void Button_Click_1(object sender, RoutedEventArgs e) //Voor het registreren.
        {
            NavigationService.Navigate(new Uri("/Register.xaml", UriKind.Relative));
        }
    }
}