using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BücherContracts
{
    public interface IBuchSpeicher
    {
        bool Speichern(IFavorisierbaresBook buch);
        bool Entfernen(IFavorisierbaresBook buch);
        List<IFavorisierbaresBook> Laden();
    }
}