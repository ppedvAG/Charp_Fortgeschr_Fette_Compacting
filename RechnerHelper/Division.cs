using RechnerContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnerHelper
{
    public class Division : IRechenoperation
    {

        public const char SymbolConst = '/';

        public char Symbol => SymbolConst;

        public double Berechne(IFormel formel)
        {
            if (formel.ZahlRechts == 0) throw new DivideByZeroException("Es darf nicht durch 0 geteilt werden!");
            return formel.ZahlLinks / formel.ZahlRechts;
        }
    }
}
