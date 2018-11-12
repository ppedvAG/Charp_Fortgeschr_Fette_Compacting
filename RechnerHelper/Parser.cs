using RechnerContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnerHelper
{
    public class Parser : IParser
    {
        public IFormel Parse(string eingabe)
        {
            //"zahlSymbolZahl"
            char? symbol = null;
            foreach (var zeichen in eingabe)
            {
                if(zeichen != ',' && zeichen != '.' && !char.IsNumber(zeichen))
                {
                    symbol = zeichen;
                    break;
                }
            }
            if (symbol == null)
                throw new FormatException("Kein Symbol gefunden!");

            string[] teile = eingabe.Split((char)symbol);
            double zahl1 = double.Parse(teile[0]);
            double zahl2 = double.Parse(teile[1]);

            return new Formel(zahl1, zahl2, (char)symbol);
        }
    }
}
