using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
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

        SpelerLokaal lokeleSpeler = new SpelerLokaal();

        int teller = 0; //Dit is een teller voor te zien aan wie het is.
        int tellerAantalPinguins = 0; //Dit is een teller voor te te weten als het spel nog in de opzet fase is of niet.
        int aantalSpelers = 4;
        int[] punten = new int[4];
        int tellerLobbyID = 0;
        int tellerSpelerID;

        string kleurVanSpeler; //Kleur van de speler bepalen.
        string kleurAanDeBeurt;

        bool magVerplaatsen = false; //Bool voor te zien als de pinguin verplaatst mag worden.
        bool opZetFace = true; //Bool voor te zien als we in de opzet fase zitten of niet.
        bool verplaatsingsface = false; //Bool voor te zien als we in de aanduidingsfase of verplaatsingsface zitten.
        bool ChangeOpzetFaceFirstTime = false;

        DispatcherTimer newTimer = new DispatcherTimer(); // creating timer instance

        ServiceReference1.DTOGameState gameState = new ServiceReference1.DTOGameState();

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
            SpelerLokaal hulpSpeler = new SpelerLokaal();
            string[] hulpString = new string[4];
            hulpString = hulpSpeler.ReturnSpeler();
            tellerLobbyID = 0;
            tellerSpelerID = 0;
            do
            {
                if (hulpString[3].ToString() == tellerLobbyID.ToString())
                    break;
                else
                    tellerLobbyID++;
            } while (true);
            do
            {
                if (hulpString[0].ToString() == tellerSpelerID.ToString())
                    break;
                else
                    tellerSpelerID++;
            } while (true);
            client.GetAllLobbiesCompleted += client_GetAllLobbiesCompleted;
            client.GetAllLobbiesAsync();
            
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


        #region Update Game
        void newTimer_Tick(object sender, EventArgs e)
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.GetGameStateCompleted += client_GetGameStateCompleted;
            client.GetGameStateAsync(tellerLobbyID);
            if (aantalSpelers == 4 && tellerAantalPinguins == 8 ||
                aantalSpelers == 3 && tellerAantalPinguins == 9 ||
                aantalSpelers == 2 && tellerAantalPinguins == 8)
            {
                if (ChangeOpzetFaceFirstTime == false)
                {
                    //Verander opzet fase naar speel fase.
                    //ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                    client.ChanceOpzetFaseCompleted += new EventHandler<ServiceReference1.ChanceOpzetFaseCompletedEventArgs>(client_ChanceOpzetFaseCompleted);
                    client.ChanceOpzetFaseAsync();
                    opZetFace = false;
                    ChangeOpzetFaceFirstTime = true;
                }
                
            }
        }        
        void client_UpdateGameStateCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.GetGameStateCompleted += client_GetGameStateCompleted;
            client.GetGameStateAsync(tellerLobbyID);
        }
        #endregion


        #region Make - (Grid & Map) - Completted
        void client_MakeGridCompleted(object sender, ServiceReference1.MakeGridCompletedEventArgs e)
        {
            ObservableCollection<int> hulpGrid = e.Result; //Een hulp grid aanmaken voor met de meegegeven waarden te kunnen werken.
            AddGrid(hulpGrid[0], hulpGrid[1], hulpGrid[2], hulpGrid[3]); //Geef de rows, colomns, height en width mee.
        }
        void client_MakeMapCompleted(object sender, ServiceReference1.MakeMapCompletedEventArgs e)
        {
            
        }
        #endregion

        #region Add - (Grid & Tile & Pinguin)
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

        #region Push - (Pinguin & Tile)
        void PushPinguin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (kleurAanDeBeurt == kleurVanSpeler)
            {
                if (opZetFace == false) //Er mag alleen iets gebeuren wanneer het spel bezig is.
                {
                    SpelSpeelstuk hulp = sender as SpelSpeelstuk; //Hulp speelstuk aanmaken zodat we aan de waarde van de sender kunnen.
                    hulpSpeelstuk2 = hulp;
                    //kleurVanSpeler = CheckKleurVanSpeler(kleurVanSpeler);
                    if (hulpSpeelstuk2.Kleur == kleurVanSpeler)
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
            else
            {
                MessageBox.Show("Wacht tot het jouw beurt is.");
            }
        }
        void PushTile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (kleurAanDeBeurt == kleurVanSpeler)
            {
                #region Tijdens het spel (Verplaatsen van Pinguin)
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
                #endregion
                #region Opzetface (Wanneer de pinguins geplaats moeten worden)
                if (opZetFace == true || magVerplaatsen == true) //Wanneer we een pinguin mogen plaatsen of verplaatsen.
                {
                    ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                    client.OpzetFaseCompleted += new EventHandler<ServiceReference1.OpzetFaseCompletedEventArgs>(client_OpzetFaseCompleted);
                    client.OpzetFaseAsync();

                    VorigeTegel = hulpTegel2; //Onthoud van waar de pinguin kwam.
                    hulpTegel2 = hulp; //Onthound waar de pinguin op gezet wordt.
                    magVerplaatsen = false; //De pinguin mag niet meer verpaatst worden.

                    punten[teller] += hulp.RandomNummer;
                    //kleurVanSpeler = CheckKleurVanSpeler(kleurVanSpeler);
                    //MessageBox.Show("Speler: " + kleur + "\nteller: " + teller + "\nPunten: " + punten[teller]);
                }
                #endregion
            }
            else
            {
                MessageBox.Show("Wacht tot het jouw beurt is.");
            }

        }
        #endregion


        #region Get - (GameState & AllLobies & AllIjsschots & SpelersInLobby)
        void client_GetGameStateCompleted(object sender, ServiceReference1.GetGameStateCompletedEventArgs e)
        {
            gameState = e.Result;
            #region Update Map
            int tellerRow = 0;
            int tellerColumn = 0;
            int evenOfOneven = 0; //Deze variabele dient voor te weten als de tegels op de even of oneven kollomen moeten komen.
            Visibility hulpVisibility = Visibility.Visible;

            foreach (var item in gameState.AllIjsschots)
            {
                tellerRow = item.Row;
                if (tellerRow % 2 == 0)
                    evenOfOneven = 0;
                else
                    evenOfOneven = 1;
                tellerColumn = item.Column * 2 + evenOfOneven;

                foreach (var tegel in SpelBord.Children)
                {
                    if (tegel is SpelTile)
                    {
                        SpelTile hulpTegel = tegel as SpelTile;
                        if (hulpTegel.Row == tellerRow && hulpTegel.Column == tellerColumn)
                        {
                            if (item.Visibility == "Visible")
                                hulpVisibility = Visibility.Visible;
                            else
                                hulpVisibility = Visibility.Collapsed;
                            hulpTegel.Visibility = hulpVisibility;
                            break;
                        }
                    }


                }
            }
            #endregion
            #region Update Pion
            tellerRow = 0;
            tellerColumn = 0;
            evenOfOneven = 0;
            bool bestaat = false;
            tellerAantalPinguins = 0;
            foreach (var pinguin in SpelBord.Children)
            {
                if (pinguin is SpelSpeelstuk)
                {
                    pinguin.Visibility = Visibility.Collapsed;
                    tellerAantalPinguins++;
                }

            }
            foreach (var item in gameState.AllPion)
            {
                tellerRow = item.Row;
                if (tellerRow % 2 == 0)
                    evenOfOneven = 0;
                else
                    evenOfOneven = 1;
                tellerColumn = item.Column; //item.Column * 2 + evenOfOneven
                bestaat = false;
                foreach (var pinguin in SpelBord.Children)
                {
                    if (pinguin is SpelSpeelstuk)
                    {
                        SpelSpeelstuk hulpPinguin = pinguin as SpelSpeelstuk;
                        if (hulpPinguin.Column == tellerColumn && hulpPinguin.Row == tellerRow)
                        {
                            hulpPinguin.Visibility = Visibility.Visible;
                            bestaat = true;
                            break;
                        }
                    }

                }
                if (bestaat == false)
                {
                    AddPinguin(item.Row, item.Column, kleurAanDeBeurt);
                }
            }
            #endregion
            #region Update Kleur
            kleurAanDeBeurt = gameState.KleurSpeler;
            #endregion
        }
        void client_GetAllIjschotsCompleted(object sender, ServiceReference1.GetAllIjschotsCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            MessageBox.Show("Map wordt aangepast.");

        }
        void client_GetAllLobbiesCompleted(object sender, ServiceReference1.GetAllLobbiesCompletedEventArgs e)
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            #region Enkel voor de HostPlayer
            foreach (var item in e.Result)
            {
                if (item.MapRows == tellerSpelerID && item.ID == tellerLobbyID) //MapRows is de HostID
                {

                    client.MakeMapCompleted += new EventHandler<ServiceReference1.MakeMapCompletedEventArgs>(client_MakeMapCompleted);
                    client.MakeMapAsync(tellerLobbyID);
                    client.SetKleurPerSpelerCompleted += client_SetKleurPerSpelerCompleted;
                    client.SetKleurPerSpelerAsync(tellerLobbyID);
                }
            }
            #endregion

            #region Voor iedereen
            foreach (var item in e.Result)
            {
                if (item.ID == tellerLobbyID)
                {
                    aantalSpelers = item.AantalSpelers;
                }
            }
            client.SpelerInLobbyCompleted += client_SpelerInLobbyCompleted;
            client.SpelerInLobbyAsync(tellerLobbyID);
            #endregion
        }
        void client_SpelerInLobbyCompleted(object sender, ServiceReference1.SpelerInLobbyCompletedEventArgs e)
        {
            string[] hulpSpeler = lokeleSpeler.ReturnSpeler();
            foreach (var item in e.Result)
            {
                if (item.NickName == hulpSpeler[1])
                {
                    kleurVanSpeler = item.Kleur;
                    break;
                }
            }

            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            System.Threading.Thread.Sleep(3000);
            MessageBox.Show("Plaats je pinguins op ijsschotsen met 1 vis."); //De map is nu compleet opgebouwd dus we mogen de pinguins gaan zetten.
            client.AddAllIjsschotsCompleted += client_AddAllIjsschotsCompleted;
            client.AddAllIjsschotsAsync(tellerLobbyID);
        }
        #endregion

        #region Add - (AllIjsschots & AllPion)
        void client_AddAllIjsschotsCompleted(object sender, ServiceReference1.AddAllIjsschotsCompletedEventArgs e)
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

            // timer interval specified as 1 second
            newTimer.Interval = TimeSpan.FromSeconds(1);
            // Sub-routine OnTimerTick will be called at every 1 second
            newTimer.Tick += newTimer_Tick;
            // starting the timer
            newTimer.Start();
        }
        void client2_AddPionToGameSteCompleted(object sender, ServiceReference1.AddPionToGameSteCompletedEventArgs e)
        {
            // MessageBox.Show(e.Result);
        }
        #endregion


        #region Fase - (Opzet & Set & Chance)
        void client_OpzetFaseCompleted(object sender, ServiceReference1.OpzetFaseCompletedEventArgs e)
        {
            if (e.Result == true && hulpTegel2.RandomNummer == 1) //Als het de opzetfase is en de tegel waar we op klikken heeft 1 vis.
            {
                //kleurVanSpeler = CheckKleurVanSpeler(kleurVanSpeler);
                AddPinguin(hulpTegel2.Row, hulpTegel2.Column, kleurVanSpeler); //Voeg pinguin toe.
                ServiceReference1.DTOPion pion = new ServiceReference1.DTOPion();
                pion.Column = hulpTegel2.Column;
                pion.Row = hulpTegel2.Row;
                pion.IjsschotsID = 0;
                pion.SpelerID = tellerSpelerID;
                pion.LobbyID = tellerLobbyID;
                ServiceReference1.Service1Client client2 = new ServiceReference1.Service1Client();
                client2.AddPionToGameSteCompleted += client2_AddPionToGameSteCompleted;
                client2.AddPionToGameSteAsync(tellerLobbyID, pion, kleurAanDeBeurt);
                tellerAantalPinguins++; //Zien wanneer alle pinguins geplaatst zijn.
                
            }
            else if (e.Result == false && verplaatsingsface == true) //Als we tijdens het spel zijn en de pinguin mag verplaatst worden.
            {
                VorigeTegel.Visibility = Visibility.Collapsed; //Laat de tegel van waar de pinguin kwam verdwijnen.
                hulpSpeelstuk2.Visibility = Visibility.Collapsed; //Laat ook de pinguin op die tegel verdwijnen.
                AddPinguin(hulpTegel2.Row, hulpTegel2.Column, hulpSpeelstuk2.Kleur); //Plaats de pinguin op zijn nieuwe lokatie.

                foreach (var item in gameState.AllPion)
                {
                    if (item.Column == hulpSpeelstuk2.Column && item.Row == hulpSpeelstuk2.Row)
                    {
                        item.Column = hulpTegel2.Column;
                        item.Row = hulpTegel2.Row;
                    }
                }
                foreach (var item in gameState.AllIjsschots)
                {
                    if (item.Column == (Convert.ToInt32(VorigeTegel.Column / 2)) && item.Row == VorigeTegel.Row)
                    {
                        item.Visibility = "Collapsed";
                    }
                }
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                client.UpdateGameStateCompleted += client_UpdateGameStateCompleted;
                client.UpdateGameStateAsync(tellerLobbyID, gameState);
                
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

        #region Set Kleur
        void client_SetKleurPerSpelerCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }
        #endregion
    }
}