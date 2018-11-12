using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsUndDelegates
{
    public class EventList<T> : BindingList<T> 
    {

        private EventHandler<OverflowMetaInfo> _overflow;
        public event EventHandler<OverflowMetaInfo> Overflow
        {
            //Wird aufgerufen, wenn dem Delegate-Objekt _overflow eine Methode zugiesen (registriert) wird
            add
            {
                //Lasse nicht zu, dass mehrmals die selbe Methode aufs Delegate-Objekt zugewiesen wird
                if(_overflow != null && _overflow.GetInvocationList().Contains(value))
                {
                    return;
                }
                _overflow += value;
            }
            //Wird aufgerufen, wenn dem Delegate-Objekt _overflow eine Methode entfernt (deregistriert) wird
            remove
            {
                _overflow -= value;
            }
        }

        //Snippet: prop
        public int? Limit { get; private set; }

        //Rechtsklick auf Klassenname->Quick Actions->Generate Constructor
        public EventList(int? limit = null)
        { 
            Limit = limit;
        }

        public new void Add(T item)
        {
            if(Limit != null && this.Count + 1 > Limit)
            {
                //Overflow-Event auslösen/feuern
                _overflow?.Invoke(this, new OverflowMetaInfo(Limit, item));
                return;
            }
            base.Add(item);
        }  
    }

    public class OverflowMetaInfo
    { 
        public int? MaxLimit { get; set; }
        public object Item { get; set; }

        public OverflowMetaInfo(int? maxLimit, object item)
        {
            MaxLimit = maxLimit;
            Item = item;
        }
    }
}
