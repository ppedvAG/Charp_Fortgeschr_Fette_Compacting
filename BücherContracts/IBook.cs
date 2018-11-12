using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BücherContracts
{
    public interface IFavorisierbaresBook : INotifyPropertyChanged
    {
        string ISBN { get; }
        string Titel { get; }
        List<string> Autoren { get; }
        string AutorenAlsString  {get; }
        string VorschauLink { get; }
        bool IstFavorit { get; set; }

    }
}
