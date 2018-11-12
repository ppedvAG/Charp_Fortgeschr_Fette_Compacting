using RechnerContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnerHelper
{
    public class Addition : IRechenoperation
    {
        //Langschreibweise
        //public char Symbol
        //{
        //    get
        //    {
        //        return '+';
        //    }
        //}

        //Expression Body Member (Rückgabewert der Getter-Methode der Property Symbol
        public char Symbol => '+';

        public double Berechne(IFormel formel)
        {
            return formel.ZahlLinks + formel.ZahlRechts;
        }
    }
}