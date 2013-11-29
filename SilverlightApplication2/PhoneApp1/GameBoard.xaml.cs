using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PhoneApp1
{
    public partial class GameBoard : PhoneApplicationPage
    {

        #region Aanmaken van variabelen
        SpelTile hulpTegel2; //Dit is de laats aangeklikte tegel.
        SpelTile VorigeTegel; //Dit is de op 1 na laatst aangeklitke tegel.

        SpelSpeelstuk hulpSpeelstuk2; //Dit is de laatst aangeklikte pinguin.

        int teller = 0; //Dit is een teller voor te zien aan wie het is.
        int tellerAantalPinguins = 0; //Dit is een teller voor te te weten als het spel nog in de opzet fase is of niet.
        int aantalSpelers = 4;
        int[] punten = new int[4];

        string kleur; //Kleur van de speler bepalen.

        bool magVerplaatsen = false; //Bool voor te zien als de pinguin verplaatst mag worden.
        bool opZetFace = true; //Bool voor te zien als we in de opzet fase zitten of niet.
        bool verplaatsingsface = false; //Bool voor te zien als we in de aanduidingsfase of verplaatsingsface zitten.

        #endregion

        public GameBoard()
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
            client.MakeMapCompleted += new EventHandler<ServiceReference1.MakeMapCompletedEventArgs>(client_MakeMapCompleted);
            client.MakeMapAsync();
            #endregion

            #region Zet het spel in de Opzet fase
            //Laat weten dat dit alles gebeurt is.
            client.SetOpzetFaseCompleted += new EventHandler<ServiceReference1.SetOpzetFaseCompletedEventArgs>(client_SetOpzetFaseCompleted);
            client.SetOpzetFaseAsync();
            #endregion

            for (int i = 0; i < punten.Length; i++)
            {
                punten[i] = 0;
            }
        }
        #region Make Grid & Map Completted
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
        #endregion

        #region Add Grid, Tile, Pinguin
        void AddGrid(int rows, int columns, int height, int width)
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
        }

        void AddTile(int row, int column, int randomNumber)
        {
            Grid parent = SpelBord;
            string fotoString = "\\Images\\" + randomNumber + "Vis.png"; //Locatie van afbeelding, hoeveel vissen er moeten opstaan.
            SpelTile st = new SpelTile(row, column, randomNumber); //Maak een usercontrol aan.
            st.Afbeelding = fotoString; //Geef de afbeelding mee.
            
            parent.Children.Add(st); //Voeg de tegel toe aan de grid.
            Grid.SetRow(st, row);
            Grid.SetColumn(st, column);
            Grid.SetColumnSpan(st, 2);
            st.MouseLeftButtonDown += new MouseButtonEventHandler(PushTile_MouseLeftButtonDown); //Laat een tegel reageren als op hem gedrukt wordt.
        }

        void AddPinguin(int row, int column, string kleur)
        {
            Grid parent = SpelBord;
            string fotoString = "/PhoneApp1;component/Images/Pinguwin" + kleur + ".png"; //Locatie van afbeelding, welke kleur het moet zijn.
            SpelSpeelstuk Ss = new SpelSpeelstuk(row, column, kleur); //Maak een usercontrol aan.
            Ss.Afbeelding = fotoString; //Geef de afbeelding mee.
            parent.Children.Add(Ss); //Voef de pinguin toe aan de grid.
            Grid.SetRow(Ss, row);
            Grid.SetColumn(Ss, column);
            Grid.SetColumnSpan(Ss, 2);
            Ss.MouseLeftButtonDown += new MouseButtonEventHandler(PushPinguin_MouseLeftButtonDown); //Laat een pinguin reageren als op hem gedrukt wordt.

            teller++; //Volgende speler
            if (teller == 4)
            {
                teller = 0;
            }
        }

        #endregion

        #region Push Pinguin, Tile
        void PushPinguin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (opZetFace == false) //Er mag alleen iets gebeuren wanneer het spel bezig is.
            {
                SpelSpeelstuk hulp = sender as SpelSpeelstuk; //Hulp speelstuk aanmaken zodat we aan de waarde van de sender kunnen.
                hulpSpeelstuk2 = hulp;
                kleur = CheckKleurVanSpeler(kleur);
                if (hulpSpeelstuk2.Kleur == kleur)
                {
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
                else
                {
                    MessageBox.Show("Invalid action!\nJe mag alleen een pinguin van je eigen kleur verplaatsen.");
                }
            }
            else
            {
                MessageBox.Show("Invalid action!\nJe kan geen meerdere pinguins op de zelfde tegel paatsen.");
            }
        }

        void PushTile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SpelTile hulp = sender as SpelTile; //Maak een hulptegel aan zondat we met de waarde van de sender kunnen werken.

            if (opZetFace == false && verplaatsingsface == true)  //Wanneer we een pinguin willen verplaatsen. Dit is het moment dat we op de nieuwe tegel drukken.
            {
                int rowVerschil = hulp.Row - hulpTegel2.Row;
                int columnVerschil = hulp.Column - hulpTegel2.Column;
                int zoekTussenliggendeRows;
                int zoekTussenliggendeColumns;
                if ((Math.Abs(rowVerschil) == Math.Abs(columnVerschil)) || (hulp.Row == hulpTegel2.Row))
                {
                    magVerplaatsen = true;
                    if (rowVerschil > 0)
                        zoekTussenliggendeRows = 1;
                    else
                        zoekTussenliggendeRows = -1;
                    if (columnVerschil > 0)
                        zoekTussenliggendeColumns = 1;
                    else
                        zoekTussenliggendeColumns = -1;


                    for (int i = 1; i <= Math.Abs(columnVerschil); i++)
                    {
                        foreach (var pinguin in SpelBord.Children)
                        {
                            SpelSpeelstuk eenSpeelstuk = pinguin as SpelSpeelstuk;
                            if (eenSpeelstuk != null)
                            {
                                if ((Math.Abs(rowVerschil) == Math.Abs(columnVerschil) &&
                                    Grid.GetRow(eenSpeelstuk) == hulpTegel2.Row + i * zoekTussenliggendeRows &&
                                    Grid.GetColumn(eenSpeelstuk) == hulpTegel2.Column + i * zoekTussenliggendeColumns) ||
                                    (hulp.Row == hulpTegel2.Row &&
                                    Grid.GetRow(eenSpeelstuk) == hulpTegel2.Row &&
                                    Grid.GetColumn(eenSpeelstuk) == hulpTegel2.Column + i * zoekTussenliggendeColumns))
                                {
                                    magVerplaatsen = false;
                                    MessageBox.Show("Invalid action!\nJe mag niet over gaten en andere pinguins springen.");
                                    break;
                                }
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid action!\nJe mag je alleen over de horizontale en diagonale lijnen verplaantsen.");
                }


            }
            if (opZetFace == true || magVerplaatsen == true) //Wanneer we een pinguin mogen plaatsen of verplaatsen.
            {
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                client.OpzetFaseCompleted += new EventHandler<ServiceReference1.OpzetFaseCompletedEventArgs>(client_OpzetFaseCompleted);
                client.OpzetFaseAsync();

                VorigeTegel = hulpTegel2; //Onthoud van waar de pinguin kwam.
                hulpTegel2 = hulp; //Onthound waar de pinguin op gezet wordt.
                magVerplaatsen = false; //De pinguin mag niet meer verpaatst worden.

                punten[teller] += hulp.RandomNummer;
                kleur = CheckKleurVanSpeler(kleur);
                //MessageBox.Show("Speler: " + kleur + "\nteller: " + teller + "\nPunten: " + punten[teller]);
            }

        }
        #endregion

        #region Fase Opzet, Set, Chance
        void client_OpzetFaseCompleted(object sender, ServiceReference1.OpzetFaseCompletedEventArgs e)
        {
            if (e.Result == true && hulpTegel2.RandomNummer == 1) //Als het de opzetfase is en de tegel waar we op klikken heeft 1 vis.
            {
                kleur = CheckKleurVanSpeler(kleur);
                AddPinguin(hulpTegel2.Row, hulpTegel2.Column, kleur); //Voeg pinguin toe.

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

        void client_SetOpzetFaseCompleted(object sender, ServiceReference1.SetOpzetFaseCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void client_ChanceOpzetFaseCompleted(object sender, ServiceReference1.ChanceOpzetFaseCompletedEventArgs e)
        {
            MessageBox.Show("Game begin! Move your pinguins \nKlik op je pinguin en vervolgens op de tegel dat je hem wilt verplaatsen.");
        }
        #endregion


        #region Methoden
        private string CheckKleurVanSpeler(string kleur)
        {
            if (teller == 0)
                kleur = "Groen";
            else if (teller == 1)
                kleur = "Geel";
            else if (teller == 2)
                kleur = "Rood";
            else
                kleur = "Blauw";
            return kleur;
        }
        #endregion
    }
}