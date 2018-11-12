using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqUndLambda
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Maschine> maschinenListe = new List<Maschine>()
            {
                new Maschine("FX7", new DateTime(1990, 1, 1)),
                new Maschine("FX9", new DateTime(1992, 1, 1)),
                new Maschine("FX4", new DateTime(2004, 1, 1)),
                new Maschine("FX4", new DateTime(1920, 1, 1)),
                new Maschine("FX1", new DateTime(1990, 1, 1)),
            };
            
            //Aufsteigend nach Herstellungsdatum sortieren
            maschinenListe.OrderBy(SuchkriteriumsMethode);
            //Anonyme Methode statt referenzierter Methode verwenden
            maschinenListe.OrderBy((Maschine m) => { return m.Herstellungsdatum; });
            //Erst nach Herstellungsdatum und dann nach Namen sortieren
            maschinenListe =  maschinenListe.OrderBy(m => m.Herstellungsdatum).ThenBy(m => m.Name).ToList();

            Console.WriteLine("MaschinenListe nach Herstellungsdatum und Name sortiert: ");
            maschinenListe.ForEach(m => Console.WriteLine(m));

            //Nach Maschinen filtern, die nach dem 1.1.1990 hergestellt wurden
            var neuereMaschinen = maschinenListe.Where(FilterkriteriumMethode);
            //Anonyme Methode statt referenzierter Methode verwenden
            var neuereMaschinenLamda = maschinenListe.Where(m => m.Herstellungsdatum > new DateTime(1990, 1, 1)).ToList();
            Console.WriteLine("MaschinenListe nach Herstellungsdatum gefiltert: ");
            neuereMaschinenLamda.ForEach(m => Console.WriteLine(m));


            Console.ReadKey();
        }
        public static bool FilterkriteriumMethode(Maschine maschine)
        {
            return maschine.Herstellungsdatum > new DateTime(1990, 1, 1);
        }


        public static DateTime SuchkriteriumsMethode(Maschine maschine)
        {
            return maschine.Herstellungsdatum;
        }
    }


    public class Maschine
    {
        public Maschine(string name, DateTime herstellungsdatum)
        {
            Name = name;
            Herstellungsdatum = herstellungsdatum;
        }

        public string Name { get; set; }
        public DateTime Herstellungsdatum { get; set; }

        public override string ToString()
        {
            return $"{Name}, hergestellt am {Herstellungsdatum.ToShortDateString()}";
        }
    }
}
