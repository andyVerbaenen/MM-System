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
            //Maak de grid aan.
            client.MakeGridCompleted += new EventHandler<ServiceReference1.MakeGridCompletedEventArgs>(client_MakeGridCompleted);
            client.MakeGridAsync();
            #endregion

            #region AddIamge
            //Voeg de tegels toe.
            client.MakeMapCompleted +=new EventHandler<ServiceReference1.MakeMapCompletedEventArgs>(client_MakeMapCompleted);
            client.MakeMapAsync();
            #endregion

            //Laat weten dat dit alles gebeurt is.
            client.SetOpzetFaseCompleted += new EventHandler<ServiceReference1.SetOpzetFaseCompletedEventArgs>(client_SetOpzetFaseCompleted);
            client.SetOpzetFaseAsync();
            
        }

        //void client_PlacePinguinCompleted(object sender, ServiceReference1.PlacePinguinCompletedEventArgs e)
        //{
        //    //AddPinguin(1, 1, "Groen");
        //    //AddPinguin(2, 2, "Blauw");
        //    //AddPinguin(2, 4, "Geel");
        //    //AddPinguin(3, 3, "Rood");

        //    ObservableCollection<ObservableCollection<int>> hulpPinguin = e.Result;
        //    string kleur;
        //    for (int i = 0; i < 16; i++)
        //    {
        //        if (hulpPinguin[i][0] != -1 && hulpPinguin[i][1] != -1)
        //        {
        //            if (i < 4)
        //                kleur = "Groen";
        //            else if (i < 8)
        //                kleur = "Geel";
        //            else if (i < 12)
        //                kleur = "Rood";
        //            else
        //                kleur = "Blauw";
        //            AddPinguin(hulpPinguin[i][0], hulpPinguin[i][1], kleur);
        //        } 
        //    }
        //}

        void client_MakeGridCompleted(object sender, ServiceReference1.MakeGridCompletedEventArgs e)
        {
            ObservableCollection<int> hulpGrid = e.Result; //Een hulp grid aanmaken voor met de meegegeven waarden te kunnen werken.
            AddGrid(hulpGrid[0], hulpGrid[1], hulpGrid[2], hulpGrid[3]); //Geef de rows, colomns, height en width mee.
        }

        void client_MakeMapCompleted(object sender, ServiceReference1.MakeMapCompletedEventArgs e)
        {
            ObservableCollection<ObservableCollection<int>> hulpMap = e.Result; //Maak hulpmap aan voor met waarden van sender te kunnen werken.
            int evenOfOneven = 0; //Deze variabele dient voor te weten als de tegels op de even of oneven kollomen moeten komen.
            
            for (int i = 0; i < 10; i++)
            {
                int echteKollomWaarde = 0; //Omdat de collomen per twee optellen en soms starten bij 0 of 1, naargelang het even of oneven moet zijn, is het moeilijk om te weten in welke zichtbare kollom we echt zitten. Daarom deze variabele.
                for (int j = evenOfOneven; j < 19; j += 2) //+2 omdat er dubbele zoveel collomen zijn als rijen voor de kollomen van de oneven rijen te laten uitkomen tussen twee kollomen van de even rijen.
                {
                    AddTile(i, j, hulpMap[i][echteKollomWaarde]); //Add de tegel met parameters: rij, kollom, en aantal vissen.
                    echteKollomWaarde++; //We zijn nu 1 kollom verder dus daarom + 1.
                }
                if (evenOfOneven == 0) //Hebben we net een even rij gehad, dan wordt deze nu oneven.
                    evenOfOneven = 1;
                else
                    evenOfOneven = 0; //Hebben we een oneven rij gehad, dan wordt deze nu even.
            }
            MessageBox.Show("Plaats je pinguins op ijsschotsen met 1 vis."); //De map is nu compleet opgebouwd dus we mogen de pinguins gaan zetten.
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
                SpelBord.ColumnDefinitions.Add(gridCol[i]); //Add colomns to the grid.
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

        //Button AddButton(string caption, int row, int column, Grid parent)
        //{
        //    Button button = new Button();
        //    button.Content = caption;
        //    parent.Children.Add(button);
        //    Grid.SetRow(button, row);
        //    Grid.SetColumn(button, column);
        //    return button;
        //}
          

        //void client_RolDobbelsteenCompleted(object sender, ServiceReference1.RolDobbelsteenCompletedEventArgs e)
        //{
        //    int getal = (int)e.Result;
        //    MessageBox.Show(getal.ToString());
        //}

    
        
        void AddTile(int row, int column, int randomNumber)
        {
            Grid parent = SpelBord;
            string fotoString = "/Pinguin;component/Images/" + randomNumber + "Vis.png"; //Locatie van afbeelding, hoeveel vissen er moeten opstaan.
            SpelTile st = new SpelTile(row, column, randomNumber); //Maak een usercontrol aan.
            st.Afbeelding = fotoString; //Geef de afbeelding mee.
            parent.Children.Add(st); //Voeg de tegel toe aan de grid.
            Grid.SetRow(st, row); 
            Grid.SetColumn(st, column);
            Grid.SetColumnSpan(st, 2);
            st.MouseLeftButtonDown += new MouseButtonEventHandler(PushTile_MouseLeftButtonDown); //Laat een tegel reageren als op hem gedrukt wordt.
        }
        
        SpelTile hulpTegel2;
        SpelTile VorigeTegel;
        bool magVerplaatsen = false;

        void PushTile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SpelTile hulp = sender as SpelTile; //Maak een hulptegel aan zondat we met de waarde van de sender kunnen werken.
            
            if (opZetFace == false && verplaatsingsface == true) //Wanneer we een pinguin willen verplaatsen. Dit is het moment dat we op de nieuwe tegel drukken.
            {
                int rowVerschil = hulp.Row - hulpTegel2.Row;
                int columnVerschil = hulp.Column - hulpTegel2.Column;
                int zoekTussenliggendeRows;
                int zoekTussenliggendeColumns;
                if (Math.Abs(rowVerschil) == Math.Abs(columnVerschil) || (hulp.Row == hulpTegel2.Row))
                {
                    if (rowVerschil > 0)
                        zoekTussenliggendeRows = 1;
                    else
                        zoekTussenliggendeRows = -1;
                    if (columnVerschil > 0)
                        zoekTussenliggendeColumns = 1;
                    else
                        zoekTussenliggendeColumns = -1;

                    for (int i = 0; i < Math.Abs(rowVerschil); i++)
                    {
                         
                    }

                    magVerplaatsen = true;
                }
                else
                {
                    MessageBox.Show("Invalid action!");
                }
               

            }
            if (opZetFace == true || magVerplaatsen == true) //Wanneer we een pinguin mogen plaatsen of verplaatsen.
            {
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                client.OpzetFaseCompleted+=new EventHandler<ServiceReference1.OpzetFaseCompletedEventArgs>(client_OpzetFaseCompleted);
                client.OpzetFaseAsync();
                            
                VorigeTegel = hulpTegel2; //Onthoud van waar de pinguin kwam.
                hulpTegel2 = hulp; //Onthound waar de pinguin op gezet wordt.
                magVerplaatsen = false; //De pinguin mag niet meer verpaatst worden.
            }
            
        }
        
        int teller = 1;
        int tellerAantalPinguins = 0;
        int aantalSpelers = 4;
        bool opZetFace = true;
        void client_OpzetFaseCompleted(object sender, ServiceReference1.OpzetFaseCompletedEventArgs e)
        {
            if (e.Result == true && hulpTegel2.RandomNummer == 1) //Als het de opzetfase is en de tegel waar we op klikken heeft 1 vis.
            {
                string kleur; //Kleur van de speler bepalen.
                if (teller == 1)
                    kleur = "Groen";
                else if (teller == 2)
                    kleur = "Geel";
                else if (teller == 3)
                    kleur = "Rood";
                else
                    kleur = "Blauw";
                
                AddPinguin(hulpTegel2.Row, hulpTegel2.Column, kleur); //Voeg pinguin toe.
                teller++; //Volgende speler
                if (teller == 5)
                {
                    teller = 1;
                }
                tellerAantalPinguins++; //Zien wanneer alle pinguins geplaatst zijn.
                if (aantalSpelers == 4 && tellerAantalPinguins == 8 || 
                    aantalSpelers == 3 && tellerAantalPinguins == 9 || 
                    aantalSpelers == 2 && tellerAantalPinguins == 8)
                {
                    //Verander opzet fase naar speel fase.
                    ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                    client.ChanceOpzetFaseCompleted += new EventHandler<ServiceReference1.ChanceOpzetFaseCompletedEventArgs>(client_ChanceOpzetFaseCompleted);
                    client.ChanceOpzetFaseAsync();
                    opZetFace = false;
                }
            }
            else if (e.Result == false && verplaatsingsface == true) //Als we tijdens het spel zijn en de pinguin mag verplaatst worden.
            {
                VorigeTegel.Visibility = Visibility.Collapsed; //Laat de tegel van waar de pinguin kwam verdwijnen.
                hulpSpeelstuk2.Visibility = Visibility.Collapsed; //Laat ook de pinguin op die tegel verdwijnen.
                AddPinguin(hulpTegel2.Row, hulpTegel2.Column, hulpSpeelstuk2.Kleur); //Plaats de pinguin op zijn nieuwe lokatie.
                verplaatsingsface = false; //de speler heeft gedaan met spelen.
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
            string fotoString = "/Pinguin;component/Images/Pinguwin" + kleur + ".png"; //Locatie van afbeelding, welke kleur het moet zijn.
            SpelSpeelstuk Ss = new SpelSpeelstuk(row, column, kleur); //Maak een usercontrol aan.
            Ss.Afbeelding = fotoString; //Geef de afbeelding mee.
            parent.Children.Add(Ss); //Voef de pinguin toe aan de grid.
            Grid.SetRow(Ss, row);
            Grid.SetColumn(Ss, column);
            Grid.SetColumnSpan(Ss, 2);
            Ss.MouseLeftButtonDown += new MouseButtonEventHandler(PushPinguin_MouseLeftButtonDown); //Laat een pinguin reageren als op hem gedrukt wordt.
        }

        SpelSpeelstuk hulpSpeelstuk2;
        void PushPinguin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (opZetFace == false) //Er mag alleen iets gebeuren wanneer het spel bezig is.
            {
                SpelSpeelstuk hulp = sender as SpelSpeelstuk; //Hulp speelstuk aanmaken zodat we aan de waarde van de sender kunnen.
                hulpSpeelstuk2 = hulp; 

                foreach (SpelTile tile in SpelBord.Children) //Gaat alle tegels af.
                {
                    if (hulp.Row == Grid.GetRow(tile) && hulp.Column == Grid.GetColumn(tile)) //Wanneer hij een tegel vind die gelijk is aan de coördinaten van de pinguin..
                    {
                        hulpTegel2 = tile; //Krijgt de hulptegel de coördinaten van de pinguin. Op deze manier want anders zit je met referentie problemen. 
                        break;
                    }
                }
                verplaatsingsface = true; //Nu mag de pinguin verplaatst worden.
            }
        }
    }
}
