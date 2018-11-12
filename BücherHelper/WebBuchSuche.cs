using BücherContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BücherHelper
{
    public class WebBuchSuche : IBuchSuche
    {
        public List<IFavorisierbaresBook> Finde(string suchbegriff)
        {
            HttpClient client = new HttpClient();
            string jsonResponse =  client.GetStringAsync("https://www.googleapis.com/books/v1/volumes?q=" + suchbegriff).Result;
            //Serialisierung: .NET -> string (JSON/XML), Binären
            //Deserialisierung: string -> .NET
            var result = JsonConvert.DeserializeObject<BuchSuchErgebnis>(jsonResponse);
            return result.items.ToList<IFavorisierbaresBook>();
        }
    }
}
