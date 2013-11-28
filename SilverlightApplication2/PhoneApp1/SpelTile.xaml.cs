using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;

namespace PhoneApp1
{
    public partial class SpelTile : UserControl
    {
        /* Deze UserControl classe is aangemaakt zondat als er op een tegel wordt geklilt, 
         * dat we de gegevens van deze tegel kunnen achterhalen. 
         * Zoals rij, kollom, aantalvissen,... 
         * Als we met een afbeelding werken ipv een usercontrol, dan gaat dit niet.
         */

        private string _Afbeelding;
        public string Afbeelding
        {
            get
            {
                return _Afbeelding;
            }
            set
            {
                //Zeg welke afbeelding het moet worden.
                string fotoString = "/Pinguin;component/Images/" + RandomNummer + "Vis.png";
                img.Source = new BitmapImage(new Uri(fotoString, UriKind.RelativeOrAbsolute));
                _Afbeelding = value;
            }
        }

        public int Row { get; set; }
        public int Column { get; set; }
        public int RandomNummer { get; set; }

        public SpelTile(int row, int column, int randomNumber)
        {
            RandomNummer = randomNumber;
            Row = row;
            Column = column;

            InitializeComponent();
        }
    }
}
