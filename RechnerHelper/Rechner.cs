using RechnerContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnerHelper
{
    public class Rechner : IRechner
    {

        public void Fehlermeldung(double? ergebnis)
        {
            if (ergebnis == null)
                throw new Exception("Unbekanntes Rechensymbol");
        }

        public virtual double Rechne(IFormel formel)
        {
            double? ergebnis = null;
            if(formel.Symbol == '+')
            {
                ergebnis = formel.ZahlLinks + formel.ZahlRechts;
            }
            
            Fehlermeldung(ergebnis);

            return (double)ergebnis;
        }
    }
}
