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
using System.Collections.ObjectModel;

namespace Pinguin
{
    public partial class MainPage : UserControl
    {
       
        public MainPage()
        {
            InitializeComponent();
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            
            #region AddGrid
            client.MakeGridCompleted += new EventHandler<ServiceReference1.MakeGridCompletedEventArgs>(client_MakeGridCompleted);
            client.MakeGridAsync();
            #endregion

            #region AddIamge
            client.MakeMapCompleted +=new EventHandler<ServiceReference1.MakeMapCompletedEventArgs>(client_MakeMapCompleted);
            client.MakeMapAsync();
            #endregion

            client.SetOpzetFaseCompleted += new EventHandler<ServiceReference1.SetOpzetFaseCompletedEventArgs>(client_SetOpzetFaseCompleted);
            client.SetOpzetFaseAsync();
            
        }

        void client_PlacePinguinCompleted(object sender, ServiceReference1.PlacePinguinCompletedEventArgs e)
        {
            //AddPinguin(1, 1, "Groen");
            //AddPinguin(2, 2, "Blauw");
            //AddPinguin(2, 4, "Geel");
            //AddPinguin(3, 3, "Rood");

            ObservableCollection<ObservableCollection<int>> hulpPinguin = e.Result;
            string kleur;
            for (int i = 0; i < 16; i++)
            {
                if (hulpPinguin[i][0] != -1 && hulpPinguin[i][1] != -1)
                {
                    if (i < 4)
                        kleur = "Groen";
                    else if (i < 8)
                        kleur = "Geel";
                    else if (i < 12)
                        kleur = "Rood";
                    else
                        kleur = "Blauw";
                    AddPinguin(hulpPinguin[i][0], hulpPinguin[i][1], kleur);
                } 
            }
        }

        void client_MakeGridCompleted(object sender, ServiceReference1.MakeGridCompletedEventArgs e)
        {
            ObservableCollection<int> hulpGrid = e.Result;
            AddGrid(hulpGrid[0], hulpGrid[1], hulpGrid[2], hulpGrid[3]);
        }

        void client_MakeMapCompleted(object sender, ServiceReference1.MakeMapCompletedEventArgs e)
        {
            ObservableCollection<ObservableCollection<int>> hulpMap = e.Result;
            int begin = 0;
            
            for (int i = 0; i < 10; i++)
            {
                int waarde = 0;
                for (int j = begin; j < 19; j += 2)
                {
                    //hulpMap[i][j] = new int();

                    //MessageBox.Show("i: " + i + " j: " + j +" hulpMap[i][waarde]: " + hulpMap[i][waarde] + " waarde: " + waarde);
                    AddTile(i, j, hulpMap[i][waarde]); 
                    waarde++;
                }
                if (begin == 0)
                    begin = 1;
                else
                    begin = 0;
            }
            MessageBox.Show("Plaats je pinguins op ijsschotsen met 1 vis.");
            #region AddPinguin
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.PlacePinguinCompleted += new EventHandler<ServiceReference1.PlacePinguinCompletedEventArgs>(client_PlacePinguinCompleted);
            client.PlacePinguinAsync();
            #endregion
        }

        void client_MakeMapCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            
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
          

        //void client_RolDobbelsteenCompleted(object sender, ServiceReference1.RolDobbelsteenCompletedEventArgs e)
        //{
        //    int getal = (int)e.Result;
        //    MessageBox.Show(getal.ToString());
        //}

    
        
        void AddTile(int row, int column, int randomNumber)
        {
            Grid parent = SpelBord;
            string fotoString = "/Pinguin;component/Images/" + randomNumber + "Vis.png";
         //   Image image = new Image();
          //  image.Source = new BitmapImage(new Uri(fotoString, UriKind.RelativeOrAbsolute));
            SpelTile st = new SpelTile(row, column, randomNumber);
            st.Afbeelding = fotoString;
           // parent.Children.Add(image);
            parent.Children.Add(st);
            //Grid.SetRow(image, row);
            //Grid.SetColumn(image, column);
            //Grid.SetColumnSpan(image, 2);

            Grid.SetRow(st, row);
            Grid.SetColumn(st, column);
            Grid.SetColumnSpan(st, 2);
            //image.Height = 50;
            //image.Width = 53;
            //image.HorizontalAlignment = new HorizontalAlignment();
            //image.MouseLeftButtonDown+=new MouseButtonEventHandler(tile_MouseLeftButtonDown);
            st.MouseLeftButtonDown += new MouseButtonEventHandler(PushTile_MouseLeftButtonDown);
            //image.HorizontalAlignment = "Center";
            //return image;
        }
        SpelTile hulpTegel2;
        SpelTile VorigeTegel;

        bool magVerplaatsen = false;
        void PushTile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SpelTile hulp = sender as SpelTile;
            
            if (opZetFace == false && verplaatsingsface == true)
            {
                if (hulp.Row == hulpTegel2.Row)
                {
                    magVerplaatsen = true;
                }

            }
            if (opZetFace == true || magVerplaatsen == true)
            {
                //VorigeTegel = hulpTegel2;
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                client.OpzetFaseCompleted+=new EventHandler<ServiceReference1.OpzetFaseCompletedEventArgs>(client_OpzetFaseCompleted);
                client.OpzetFaseAsync();
            
                //hulpTegel2.Visibility = Visibility.Collapsed;
                
                VorigeTegel = hulpTegel2;
                hulpTegel2 = hulp;
                magVerplaatsen = false;
            }
            
        }
        int teller = 1;
        int tellerAantalPinguins = 0;
        int aantalSpelers = 4;
        bool opZetFace = true;
        void client_OpzetFaseCompleted(object sender, ServiceReference1.OpzetFaseCompletedEventArgs e)
        {
            if (e.Result == true && hulpTegel2.RandomNummer == 1)
            {
                string kleur;
                if (teller == 1)
                    kleur = "Groen";
                else if (teller == 2)
                    kleur = "Geel";
                else if (teller == 3)
                    kleur = "Rood";
                else
                    kleur = "Blauw";
                
                AddPinguin(hulpTegel2.Row, hulpTegel2.Column, kleur);
                teller++;
                if (teller == 5)
                {
                    teller = 1;
                }
                tellerAantalPinguins++;
                if (aantalSpelers == 4 && tellerAantalPinguins == 8 || 
                    aantalSpelers == 3 && tellerAantalPinguins == 9 || 
                    aantalSpelers == 2 && tellerAantalPinguins == 8)
                {
                    ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                    client.ChanceOpzetFaseCompleted += new EventHandler<ServiceReference1.ChanceOpzetFaseCompletedEventArgs>(client_ChanceOpzetFaseCompleted);
                    client.ChanceOpzetFaseAsync();
                    opZetFace = false;
                }
            }
            else if (e.Result == false && verplaatsingsface == true)
            {
                
                //hulpTegel2.Visibility = Visibility.Collapsed;
                VorigeTegel.Visibility = Visibility.Collapsed;
                //SpelTile hulp = sender as SpelTile;
                //hulpTegel2 = hulp;
                hulpSpeelstuk2.Visibility = Visibility.Collapsed;

                AddPinguin(hulpTegel2.Row, hulpTegel2.Column, hulpSpeelstuk2.Kleur);
                verplaatsingsface = false;
            }

            
        }
        bool verplaatsingsface = false;
        void client_SetOpzetFaseCompleted(object sender, ServiceReference1.SetOpzetFaseCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void client_ChanceOpzetFaseCompleted(object sender, ServiceReference1.ChanceOpzetFaseCompletedEventArgs e)
        {
            MessageBox.Show("Game begin! Move your pinguins \nKlik op je pinguin en vervolgens op de tegel dat je hem wilt verplaatsen.");
        }
        void AddPinguin(int row, int column, string kleur)
        {
            Grid parent = SpelBord;
            string fotoString = "/Pinguin;component/Images/Pinguwin" + kleur + ".png";
            //Image image = new Image();
            //image.Source = new BitmapImage(new Uri(fotoString, UriKind.RelativeOrAbsolute));
            SpelSpeelstuk Ss = new SpelSpeelstuk(row, column, kleur);
            Ss.Afbeelding = fotoString;
            //parent.Children.Add(image);
            //Grid.SetRow(image, row);
            //Grid.SetColumn(image, column);
            //Grid.SetColumnSpan(image, 2);
            parent.Children.Add(Ss);
            Grid.SetRow(Ss, row);
            Grid.SetColumn(Ss, column);
            Grid.SetColumnSpan(Ss, 2);
            Ss.MouseLeftButtonDown += new MouseButtonEventHandler(PushPinguin_MouseLeftButtonDown);
            //image.MouseDown = "image1_MouseDown";
        }

        SpelSpeelstuk hulpSpeelstuk2;
        void PushPinguin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (opZetFace == false)
            {
                SpelSpeelstuk hulp = sender as SpelSpeelstuk;
                hulpSpeelstuk2 = hulp;

                foreach (SpelTile tile in SpelBord.Children)
                {
                    if (hulp.Row == Grid.GetRow(tile) && hulp.Column == Grid.GetColumn(tile))
                    {
                        hulpTegel2 = tile;
                        break;
                    }

                }

               // hulpTegel2 = hulp;
                //hulpTegel2.Row = hulpSpeelstuk2.Row;
                //hulpTegel2.Column = hulpSpeelstuk2.Column;

                verplaatsingsface = true;
            }
            

            //Speelstuk.X = hulp.Row;
            //Speelstuk.Y = hulp.Column;
            //Tile.X = Speelstuk.X;
            //Tile.Y = Speelstuk.Y;
        }
        //Image tempPinguin2;
        //Image tempTile2;
        //void image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    Image temp = sender as Image;
        //    tempPinguin2 = temp;
        //}
        //void tile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
            
            
        //    Image temp = sender as Image;
        //    tempTile2 = temp;
            
            
        //    tempPinguin2.Visibility = Visibility.Collapsed;
        //    // AddPinguin();

            
        //    //temp.Visibility = Visibility.Collapsed;
            
        //}
        //private void image1_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        //{
        //    this.Visibility = Visibility.Collapsed;
        //}

        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
        //    // ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        //    //client.RolDobbelsteenCompleted +=new EventHandler<ServiceReference1.RolDobbelsteenCompletedEventArgs>(client_RolDobbelsteenCompleted);
        //    //client.RolDobbelsteenAsync(); 
        
        //    //image00.Visibility = Visibility.Collapsed;
        //}

        ////private void image00_Tap(object sender, GestureEventArgs e)
        ////{
        ////    Image send = sender as Image;
        ////    send.Visibility = Visibility.Collapsed;
        ////}
    }
}
