using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RechnerContracts;

namespace RechnerHelper
{
    public class FlexRechner : Rechner
    {
        public List<IRechenoperation> Rechenoperationen { get; set; }

        public FlexRechner()
        {
            Rechenoperationen = new List<IRechenoperation>();
            Rechenoperationen.Add(new Addition());
            Rechenoperationen.Add(new Division());
        }

        public override double Rechne(IFormel formel)
        {
            double? ergebnis = null;
            foreach (var item in Rechenoperationen) 
            {
                if(item.Symbol == formel.Symbol)
                {
                    ergebnis = item.Berechne(formel);
                }
            }
            base.Fehlermeldung(ergebnis);

            return (double)ergebnis;
        }
    }
}
