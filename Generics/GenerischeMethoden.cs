using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    public static class GenerischeMethoden
    {

        #region Nicht-Erweiterungsmethoden
        public static void Swap<T>(ref T x, ref T y)
        {
            T xCopy = x;
            x = y; //x => Wort2
            y = xCopy; // y => Wort1
        }

        public static void SwapOut<T>(T x, T y, out T xOut, out T yOut)
        {
            xOut = y;
            yOut = x;
        }


        public static Tuple<T, T> SwapTuple<T>(T x, T y)
        {
            return new Tuple<T, T>(y, x);
        }
        #endregion



        #region Erweiterungsmethoden

        //Erweiterungsmethode: muss in statischer Klasse definiert sein
        //Erster Parameter mit this gibt an, für Objekte welcher Klasse die Methoden
        //aufgerufen werden kann.
        public static void SwapExt<T>(this ref T x, ref T y) where T : struct // T muss ein Wertetyp sein
        {  
            T xCopy = x;
            x = y; //x => Wort2
            y = xCopy; // y => Wort1
        }

        //Erweiterungsmethode für Integer
        public static int Quersumme(this int zahl)
        {
            string intAsString = zahl.ToString();
            int summe = 0;
            foreach (var item in intAsString)
            {
                summe += item;
            }
            return summe;
        }

        //Erweiterungsmthode, um einem Dictionary mit Value-Type ICollection ein Item hinzuzufügen, 
        //welches dann automatisch der Liste hinzugefügt wird
        public static void AddItemToList<TKey, TListType, TTypeInList>(this IDictionary<TKey, TListType> dict,  //Dictionary von dem die Methode aufgerufen wird
                                                            TKey key, //Datentyp für den Key 
                                                            TTypeInList value) //Datentyp für die Daten innerhalb der Liste
                                                            where TListType : ICollection<TTypeInList>,new() //new (): Datentyp muss einen parameterlosen Konstruktor bestizen
        {
            //Gibt es den Key schon?
            if (dict.ContainsKey(key))
            {
                dict[key].Add(value);
            }
            else
            {
                dict.Add(key, new TListType() { value });
            }
        }
        #endregion

    }
}
