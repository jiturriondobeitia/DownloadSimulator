using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadSimulator
{
    internal class Fitxategia
    {
        // Propietateak.
        public string Izena { get; set; }
        public int Tamaina { get; set; }

        // Konstruktorea.
        public Fitxategia(string izena, int tamaina)
        {
            this.Izena = izena;
            this.Tamaina = tamaina;
        }
    }
}
