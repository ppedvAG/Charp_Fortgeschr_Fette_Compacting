using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BücherContracts
{
    public interface IBuchSuche
    {
        List<IFavorisierbaresBook> Finde(string suchbegriff);
    }
}
