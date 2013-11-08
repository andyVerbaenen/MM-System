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
using System.Windows.Media.Imaging;

namespace Pinguin
{
    public partial class MainPage : UserControl
    {
       
        public MainPage()
        {
            InitializeComponent();

            #region AddGrid
            AddGrid(10, 20, 50, 30);
            #endregion

            #region AddIamge
            int begin = 0;
            for (int i = 0; i < 10; i++)
            {

                for (int j = begin; j < 19; j += 2)
                {
                    AddImage(i, j);
                }
                if (begin == 0)
                    begin = 1;
                else
                    begin = 0;
            }
            #endregion

        }
        
        Grid AddGrid(int rows, int columns, int height, int width)
        {
            SpelBord.HorizontalAlignment = HorizontalAlignment.Center;
            SpelBord.VerticalAlignment = VerticalAlignment.Center;

            #region Create Columns
            ColumnDefinition[] gridCol = new ColumnDefinition[columns];
            for (int i = 0; i < columns; i++)
            {
                gridCol[i] = new ColumnDefinition();
                gridCol[i].Width = new GridLength(width);
                SpelBord.ColumnDefinitions.Add(gridCol[i]);
            }
            #endregion

            #region Create Rows
            RowDefinition[] gridRow = new RowDefinition[rows];
            for (int i = 0; i < rows; i++)
            {
                gridRow[i] = new RowDefinition();
                gridRow[i].Height = new GridLength(height);
                SpelBord.RowDefinitions.Add(gridRow[i]);
            }
            #endregion
            return SpelBord;
        }

        Button AddButton(string caption, int row, int column, Grid parent)
        {
            Button button = new Button();
            button.Content = caption;
            parent.Children.Add(button);
            Grid.SetRow(button, row);
            Grid.SetColumn(button, column);
            return button;
        }
          

        void client_RolDobbelsteenCompleted(object sender, ServiceReference1.RolDobbelsteenCompletedEventArgs e)
        {
            int getal = (int)e.Result;
            MessageBox.Show(getal.ToString());
        }

    
        Random random = new Random();
        Image AddImage(int row, int column)
        {
            Grid parent = SpelBord;
            int randomNumber = random.Next(1, 4);
            string fotoString = "/Pinguin;component/Images/" + randomNumber + "Vis.png";
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(fotoString, UriKind.RelativeOrAbsolute));
            parent.Children.Add(image);
            Grid.SetRow(image, row);
            Grid.SetColumn(image, column);
            Grid.SetColumnSpan(image, 2);
            image.Height = 50;
            image.Width = 53;
            image.HorizontalAlignment = new HorizontalAlignment();
            //image.HorizontalAlignment = "Center";
            return image;
        }

        private void image1_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void image1_Click(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show("Kaka");
            this.Visibility = Visibility.Collapsed;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
             ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.RolDobbelsteenCompleted += new EventHandler<ServiceReference1.RolDobbelsteenCompletedEventArgs>(client_RolDobbelsteenCompleted);
            client.RolDobbelsteenAsync();
        
            //image00.Visibility = Visibility.Collapsed;
        }

        //private void image00_Tap(object sender, GestureEventArgs e)
        //{
        //    Image send = sender as Image;
        //    send.Visibility = Visibility.Collapsed;
        //}
    }
}
