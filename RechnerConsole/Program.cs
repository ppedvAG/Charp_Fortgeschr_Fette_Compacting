using RechnerContracts;
using RechnerHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Bootstrapping
            IParser parser = new Parser();
            IRechner rechner = new FlexRechner();

            //evtl. Plugins importierenm per Reflection
            Console.Write("Bitte geben Sie die Aufgabe ein: ");

            //Dateneingabe
            string aufgabe = Console.ReadLine();

            //Parsen
            IFormel formel = parser.Parse(aufgabe);

            //Berechnung
            double ergebnis = rechner.Rechne(formel);

            //Aufgabne
            Console.WriteLine($"\nErgebnis: {ergebnis}");

            Console.ReadKey();

        }
    }

    #region Mock-Methode, solange die Klassen für Rechner, Formel und Parser noch nicht fertig sind

    public class ParserMock : IParser
    {
        public IFormel Parse(string eingabe)
        {
            return new FormelMock();
        }
    }
    public class FormelMock : IFormel
    {
        public double ZahlLinks => 10;

        public double ZahlRechts => 5;

        public char Symbol => '+';
    }
    public class RechnerMock : IRechner
    {
        public double Rechne(IFormel formel)
        {
            return 15;
        }
    }
    #endregion 
}
