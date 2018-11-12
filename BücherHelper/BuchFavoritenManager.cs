using BücherContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BücherHelper
{
    public class BuchFavoritenManager : IFavoriteBooksManager
    {
        private List<IFavorisierbaresBook> _favoriten;

        private IBuchSpeicher _buchSpeicher;

        //ServerExplorer->Create New SQL Server Database->(localdb)\mssqllocaldb

        public BuchFavoritenManager()
        {
            _favoriten = new List<IFavorisierbaresBook>();
            _buchSpeicher = new BuchDBSpeicher();

            //Daten laden
            List<IFavorisierbaresBook> geladeneBücher = _buchSpeicher.Laden();
            if(geladeneBücher != null && geladeneBücher.Count > 0)
            {
                _favoriten = geladeneBücher;
            }

        }

        public bool EntferneFavorit(IFavorisierbaresBook zuEntfernendeBuch)
        {
            IFavorisierbaresBook book = _favoriten.Find(b => b.ISBN == zuEntfernendeBuch.ISBN);
            if (book != null)
            {
                _favoriten.Remove(book);
                _buchSpeicher.Entfernen(book);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Neues Buch zu den Favoriten hinzufügen
        /// </summary>
        /// <param name="hinzuzufügendesBuch"></param>
        /// <returns>true: Buch erfolgreich hinzugefügt</returns>
        public bool FügeAlsFavoritHinzu(IFavorisierbaresBook hinzuzufügendesBuch)
        {
            if (!_favoriten.Any(b => b.ISBN == hinzuzufügendesBuch.ISBN))
            {
                _favoriten.Add(hinzuzufügendesBuch);
                _buchSpeicher.Speichern(hinzuzufügendesBuch);
                return true;
            }
            return false;
        }

        public List<IFavorisierbaresBook> GetFavoriten()
        {
            //Kopierkonstrutkor
            return new List<IFavorisierbaresBook>(_favoriten);
        }
    }
}
