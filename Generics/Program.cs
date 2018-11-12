using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            EvolutionDerGenerics();
            MyListTesten();
            ExistierendeGenerischeKlassen();
            ListDictionaryTesten();
            TesteGenerischeMethoden();

            Console.ReadKey();
        }
        #region TesteGenerischeMethoden
        private static void TesteGenerischeMethoden()
        {
            Console.WriteLine("-----TesteGenerischeMethoden-----");

            int zahl = 5;
            int zahl2 = 10;
            //GenerischeMethoden.Swap<int>(ref zahl, ref zahl2);
            //Project->Properties->Build->Advanced->C# Version auf 7.2!!
            zahl.SwapExt(ref zahl2);
            Console.WriteLine($"Zahl1: {zahl}");
            Console.WriteLine($"Zahl2: {zahl2}");

            string wort1 = "Wort1";
            string wort2 = "Wort2";
            GenerischeMethoden.Swap(ref wort1, ref wort2);
            //GenerischeMethoden.SwapOut<string>(wort1, wort2, out wort1, out wort2);

            Console.WriteLine($"Wort1: {wort1}");
            Console.WriteLine($"Wort2: {wort2}");
        }
        #endregion

        #region Beispiel für OutParameter
        public static void OutParameter()
        {
            //Umständliche Variante der Swap-Methode mit Tuples
            string wort1 = "Wort1";
            string wort2 = "Wort2";
            Tuple<string, string> result = GenerischeMethoden.SwapTuple<string>(wort1, wort2);
            wort1 = result.Item2;
            wort2 = result.Item1;


            //Eingabe wird solange wiederholt, bis die richtige Zahl eingegeben wurde
            int geparsteZahl;
            while (!int.TryParse(Console.ReadLine(), out geparsteZahl))
            {
                Console.WriteLine("Zahl konnte nicht geparst werden. Geben Sie eine neue ein: ");
            }
            Console.WriteLine($"geparste Zahl: {geparsteZahl}");

        }
        #endregion

        #region ListDictionaryTesten
        private static void ListDictionaryTesten()
        {

            Dictionary<int, List<string>> MaschinenKäuferTabelle = new Dictionary<int, List<string>>();

            #region Benutzung mit herkömmlicher Dictionary-Klasse

            MaschinenKäuferTabelle.Add(258, new List<string>() { "Hans Meier" });

            if (MaschinenKäuferTabelle.ContainsKey(258))
            {
                MaschinenKäuferTabelle[258].Add("Anja Schulz");
            }
            else
            {
                MaschinenKäuferTabelle.Add(258, new List<string>() { "Anja Schulz" });
            }
            #endregion

            //Nutzung der Erweiterungsmethode
            MaschinenKäuferTabelle.AddItemToList(258, "Friedrich Hamer");

            Console.WriteLine("--------ListDictionary testen--------");

            ListDictionary<int, string> meineTabelle = new ListDictionary<int, string>();
            meineTabelle.Add(20, "Peter Schulz");
            meineTabelle.Add(20, "Anja Schulz");
            meineTabelle.Add(20, "Anja Meier");

            Console.WriteLine("Käufer von Maschine mit ID 20");
            foreach (var item in meineTabelle[20])
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("--------FlexListDictionary testen--------");


            var flexTabelle = new FlexListDictionary<int, string, ObservableCollection<string>>();

            flexTabelle.Add(200, "Testperson");
            flexTabelle.Add(200, "Testperson2");
            flexTabelle[200].ToList().ForEach(element => Console.WriteLine(element));

        }
        #endregion

        #region ExistierendeGenerischeKlassen

        private static void ExistierendeGenerischeKlassen()
        {
            Console.WriteLine("--------Beispiele für Generische Klassen---------");

            List<int> zahlenListe;
            Stack<string> zahlenHaufen = new Stack<string>();
            zahlenHaufen.Push("Maschine 1");
            zahlenHaufen.Push("Maschine 2");
            string letzteMaschine = zahlenHaufen.Pop();
            Collection<int> coll = new Collection<int>();
            //Liste mit CollectionChanged-Event, wichtig z.B. für WPF-Binding
            ObservableCollection<string> beobachtbareListe = new ObservableCollection<string>();
            beobachtbareListe.CollectionChanged += BeobachtbareListe_CollectionChanged;
            //Ähnlich zu ObservableCollection, wird in WindowsForms z.B. für DataGridView benutzt
            BindingList<string> windowsFormVarianteVonObservalbeCollection;

            //Tabelle abbilden
            Dictionary<string, int> MaschinenVerkäufe = new Dictionary<string, int>();

            //Entspricht Dictionary<object, object>
            Hashtable hashtable = new Hashtable();

            //Mehrere Werte in einem Objekt abspeichern 
            Tuple<bool, double> ss;

            Console.WriteLine("Division: " + Dividiere(5.0, 0).Item2);

            //Generische Delegate-Typen
            Action<int> action;
            Func<string, int> funtion;

        }

        public static Tuple<bool, double> Dividiere(double z1, double z2)
        {
            //Division einer Gleitkommazahl ergibt per Definition Unendlich (8)
            //Daher muss dieser Fall explizit behandelt werden
            if (z2 == 0)
            {
                return new Tuple<bool, double>(false, 0);
            }
            return new Tuple<bool, double>(true, z1 / z2);
        }

        private static void BeobachtbareListe_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("Element wurde verändert");
        }
        #endregion

        #region MyListTesten

        private static void MyListTesten()
        {
            Console.WriteLine("--------MyListTesten---------");

            MyList<int> liste = new MyList<int>(20);
            liste.Add(30);
            liste.Add(40);
            liste.Add(20);
            liste.Remove(30);

            liste[1] = 20;

            Console.Write("Liste ausgeben:");
            foreach (var item in liste)
            {
                Console.Write($"{item},");
            }
            Console.WriteLine();
        }
        #endregion

        #region EvolutionDerGenerics
        private static void EvolutionDerGenerics()
        {
            Console.WriteLine("---------Evolution der Generics----------");

            string[] personenNamen = new string[] { "Hans", "Peter", "Markus" };

            //Arrays kopieren
            string[] personenNamenNew = new string[4];
            Array.Copy(personenNamen, personenNamenNew, 3);
            personenNamenNew[3] = "Anja";


            object irgendeinObjekt = 5;
            irgendeinObjekt = DateTime.Now;

            //In Arraylist werden alle Elemente in einem object-Array gespeichert
            ArrayList gemischteListe = new ArrayList();
            gemischteListe.Add("Hans");
            gemischteListe.Add("Peter");
            gemischteListe.Remove("Hans");
            gemischteListe.Add(20);
            gemischteListe.Add(30);
            gemischteListe.Add(true);

            int summe = 0;
            //Summiere alle Integer-Instanzen in der Arraylist auf
            foreach (var item in gemischteListe)
            {
                if (item is int gecasteteVariable)
                {
                    summe += gecasteteVariable;
                }
            }
            Console.WriteLine($"Summe: {summe}");

            Tablette tablette = new Tablette("Sinupret", 20);
            //Tabellete2 bekommt alle Werte von tablette-Instanz kopiert
            Tablette tablette2 = tablette;

            tablette.ID = 4;
            Console.WriteLine($"ID von {nameof(tablette)}: {tablette.ID}");
            Console.WriteLine($"ID von {nameof(tablette2)}: {tablette2.ID}");

            //Boxing (Wertetypen in Referenztypen verpacken -> kostspielig!)
            object z1 = 4;
            object z2 = 4;

            Console.WriteLine($"z1 == z2??: {z1 == z2}");

            //Unboxing (Referenztypen zurückkonvertieren in Wertetypen -> kostspielig!)
            if ((int)z1 == (int)z2)
            {
                Console.WriteLine("Nach Unboxing: z1 ist gleich z2");
            }
        }
        #endregion
    }


    #region Beispiel für Wertetyp
    public struct Tablette //Basisklasse aller Structs ist ValueType
    {
        public string Name { get; set; }
        public int ID { get; set; }

        public Tablette(string name, int id)
        {
            Name = name;
            ID = id;
        }
    }
    #endregion

}
