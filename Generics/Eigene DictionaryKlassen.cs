using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    /// <summary>
    /// Dictionary, welches einem Key eine Liste von Values zuordnet. Value-Type ist immer List
    /// </summary>
    /// <typeparam name="TKey">Datentyp für den Key</typeparam>
    /// <typeparam name="TValue">Datentyp, welcher in der Liste gespeichert wird</typeparam>
    public class ListDictionary<TKey, TValue> : Dictionary<TKey,List<TValue>>
    {
        /// <summary>
        /// Füge zu einem Key einen neuen Value ein.
        /// </summary>
        /// <param name="key">Wenn der Key noch nicht existiert, wird er neu angelegt</param>
        /// <param name="value">Der Value wird in die Liste hinzugefügt</param>
        public void Add(TKey key, TValue value)
        {
            //Gibt es den Key schon?
            if(this.ContainsKey(key))
            {
                this[key].Add(value);
            }
            else
            {
                this.Add(key, new List<TValue>() { value });
            }
        }
    }

    /// <summary>
    /// Dictionary, welches einem Key eine Liste von Values zuordnet. Datentyp der Liste ist frei wählbar
    /// </summary>
    /// <typeparam name="TKey">Datentyp für den Key</typeparam>
    /// <typeparam name="TListType">Listen-Datentyp für den Value</typeparam>
    /// <typeparam name="TListItemType">Datentyp, welcher in der Liste gespeichert wird</typeparam>
    public class FlexListDictionary<TKey,TListItemType,TListType> : Dictionary<TKey, TListType> where TListType : IList<TListItemType>,new()
    {
        /// <summary>
        /// Füge zu einem Key einen neuen Value ein.
        /// </summary>
        /// <param name="key">Wenn der Key noch nicht existiert, wird er neu angelegt</param>
        /// <param name="value">Der Value wird in die Liste hinzugefügt</param>
        public void Add(TKey key, TListItemType value)
        {
            //Gibt es den Key schon?
            if (this.ContainsKey(key))
            {
                this[key].Add(value);
            }
            else
            {
                this.Add(key, new TListType() { value });
            }
        }
    }
}