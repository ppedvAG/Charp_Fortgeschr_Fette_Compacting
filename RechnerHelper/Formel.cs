using RechnerContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnerHelper
{
    public class Formel : IFormel
    {
      
        public double ZahlLinks { get; private set; }

        public double ZahlRechts  { get; private set; }

        public char Symbol { get; private set; }

        public Formel(double zahlLinks, double zahlRechts, char symbol)
        {
            ZahlLinks = zahlLinks;
            ZahlRechts = zahlRechts;
            Symbol = symbol;
        }
    }
}
