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
            Service1Client client = null;
            try
            {
                client = new Service1Client();
                client.GetDataCompleted += client_GetDataCompleted;
                client.GetDataAsync(1);

            }
            catch (Exception)
            {

                throw;
            }
        }

        void client_GetDataCompleted(object sender, GetDataCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
