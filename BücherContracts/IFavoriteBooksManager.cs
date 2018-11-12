using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BücherContracts
{
    public interface IFavoriteBooksManager
    {
        bool FügeAlsFavoritHinzu(IFavorisierbaresBook book);
        bool EntferneFavorit(IFavorisierbaresBook book);
        List<IFavorisierbaresBook> GetFavoriten();
    }
}
