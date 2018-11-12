using BücherContracts;
using BücherHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BüchersucheManager
{
    public partial class Form1 : Form
    {
        IBuchSuche _buchSucher;
        IFavoriteBooksManager _favoritenManager;
        BindingList<IFavorisierbaresBook> _buchTreffer;

        public Form1()
        {
            InitializeComponent();
            //Bootstrapping: Module vorbereiten
            _buchSucher = new WebBuchSuche();
            _favoritenManager = new BuchFavoritenManager();

            //Automatische Erstellung von Spalten deaktivieren
            dataGridBücher.AutoGenerateColumns = false;

            //Definieren, welche Spalte im DataGridView welche Property des Objekts anzeigen soll
            DataGridViewTextBoxColumn titleColumn = new DataGridViewTextBoxColumn();
            titleColumn.HeaderText = "Titel";
            titleColumn.DataPropertyName = nameof(IFavorisierbaresBook.Titel);
            dataGridBücher.Columns.Add(titleColumn);

            DataGridViewTextBoxColumn authorsColumn = new DataGridViewTextBoxColumn();
            authorsColumn.HeaderText = "Autoren";
            authorsColumn.DataPropertyName = nameof(IFavorisierbaresBook.AutorenAlsString);
            dataGridBücher.Columns.Add(authorsColumn);

            DataGridViewButtonColumn buttonColumm = new DataGridViewButtonColumn();
            buttonColumm.HeaderText = "Link";
            buttonColumm.Text = "Vorschau";
            buttonColumm.UseColumnTextForButtonValue = true;
            dataGridBücher.Columns.Add(buttonColumm);

            DataGridViewCheckBoxColumn cbColumn = new DataGridViewCheckBoxColumn();
            cbColumn.HeaderText = "Favorit";
            cbColumn.DataPropertyName = nameof(IFavorisierbaresBook.IstFavorit);
            dataGridBücher.Columns.Add(cbColumn);


            dataGridBücher.CellClick += DataGridBücher_CellClick;

        }

        private void DataGridBücher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                Process.Start(_buchTreffer[e.RowIndex].VorschauLink);
            }
        }

        private void buttonSuche_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                return;

            var ergebnisse = _buchSucher.Finde(textBox1.Text);
            //Zum Anzeigen der Ergebnisse im DataGridView, das Listen-Objekte zunächst in eine BindingList kopieren mittels
            //Kopierkonstruktors
            _buchTreffer = new BindingList<IFavorisierbaresBook>(ergebnisse);

            //Als Datenquelle einer DataGridViews sollte eine BindingList oder ObservableCollection verwendet werden,
            //denn diese Klassen besitzen ein CollectionChanged- bzw. ListChanged-Event, wodurch das DataGridView darauf reagieren kann,
            //wenn sich Elemente verändern (add/remove etc.)
            dataGridBücher.DataSource = _buchTreffer; 
      
            _buchTreffer.ListChanged += _buchTreffer_ListChanged;
        }

        private void _buchTreffer_ListChanged(object sender, ListChangedEventArgs e)
        {
            //Prüfen ob sich die Eigenschaft IstFavorit eines Elements innerhalb der BindingList verändert hat
            if (e.ListChangedType == ListChangedType.ItemChanged && e.PropertyDescriptor.Name == nameof(IFavorisierbaresBook.IstFavorit))
            {

                IFavorisierbaresBook book = _buchTreffer[e.NewIndex];

                //False => True: Als Favorit hinzufügen
                if (book.IstFavorit)
                {
                    _favoritenManager.FügeAlsFavoritHinzu(book);
                }
                //True => False: Als Faveorit entfernen
                else
                {
                    _favoritenManager.EntferneFavorit(book);
                }
            }
        }

        private void buttonFavoriten_Click(object sender, EventArgs e)
        {
            List<IFavorisierbaresBook> favoriten = _favoritenManager.GetFavoriten();

            //Wenn es Favoriten gibt, diese im DataGridView anzeigen
            if (favoriten.Count > 0)
            {
                if(_buchTreffer != null)
                    _buchTreffer.ListChanged -= _buchTreffer_ListChanged;

                dataGridBücher.DataSource = _buchTreffer = new BindingList<IFavorisierbaresBook>(favoriten);
                _buchTreffer.ListChanged += _buchTreffer_ListChanged;
            }         
        }
    }
}
