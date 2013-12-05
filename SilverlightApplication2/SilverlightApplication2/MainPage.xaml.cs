using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SilverlightApplication2.ServiceReference1;

namespace SilverlightApplication2
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            ServiceReference1.Service1Client cl = new Service1Client();
            cl.AddPionToGameSteCompleted += cl_AddPionToGameSteCompleted;
            Pion p = new Pion();
            p.Ijsschots= 33;
            p.LobbyID = 5;
            p.Row = 88;
            p.SpelerID = 1;
            p.Column = 4;
            
            cl.AddPionToGameSteAsync(p);
        }

        void cl_AddPionToGameSteCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

       
    }
}
