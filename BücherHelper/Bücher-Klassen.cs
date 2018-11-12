using BücherContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BücherHelper
{

    //Die für die Deserialiserung notwendigen Klassen mittels http://json2csharp.com automatisch erstellen lassen.
    //Als JSON das Ergebis einer Beispiel-Abfrage des Webservices verwenden.

    public class BuchSuchErgebnis
    {
        public Book[] items { get; set; }
    }

    public class Book : IFavorisierbaresBook
    {
        public Volumeinfo volumeInfo { get; set; }


        //Getter-Properties für DataGridView
        public string ISBN => volumeInfo.industryIdentifiers[0].identifier;

        public string Titel => volumeInfo.title;

        public List<string> Autoren => volumeInfo.authors?.ToList();

        public string VorschauLink => volumeInfo.previewLink;

        //propfull
        private bool _istFavorit = false;
        public bool IstFavorit
        {
            get { return _istFavorit; }
            set
            {
                if (_istFavorit != value)
                {
                    _istFavorit = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IstFavorit)));
                }
            }
        }

        public string AutorenAlsString => Autoren != null ? string.Join(", ", Autoren) : " ";

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Volumeinfo
    {
        public string title { get; set; }
        public string[] authors { get; set; }
        public Industryidentifier[] industryIdentifiers { get; set; }
        public string previewLink { get; set; }
    }

    public class Industryidentifier
    {
        public string identifier { get; set; }
    }
}