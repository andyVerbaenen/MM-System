﻿using System;
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
    public partial class SpelSpeelstuk : UserControl
    {
        /* Deze UserControl classe is aangemaakt zondat als er op een pinguin wordt geklilt, 
         * dat we de gegevens van deze pinguin kunnen achterhalen. 
         * Zoals rij, kollom, kleur,... 
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
                string fotoString = "/Pinguin;component/Images/Pinguwin" + Kleur + ".png";
                img2.Source = new BitmapImage(new Uri(fotoString, UriKind.RelativeOrAbsolute));
                _Afbeelding = value;
            }
        }
        
        public int Row { get; set; }

        public int Column { get; set; }
        public string Kleur { get; set; }
        public SpelSpeelstuk(int row, int column, string kleur)
        {
            Kleur = kleur;
            Row = row;
            Column = column;
            InitializeComponent();
        }
    }
}
