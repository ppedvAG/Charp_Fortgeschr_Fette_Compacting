using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventsUndDelegates
{
    public partial class Form1 : Form
    {

        EventList<string> _personenListe;

        public Form1()
        {
            InitializeComponent();
            _personenListe = new EventList<string>(5);
            //ListBox mit PersonenListe verheiraten (synchronisieren)
            listBox1.DataSource = _personenListe;
            _personenListe.Overflow += _personenListe_Overflow;
            _personenListe.Overflow += _personenListe_Overflow1;
        }

        private void _personenListe_Overflow1(object sender, OverflowMetaInfo e)
        {
            this.BackColor = Color.Red;
        }

        private void _personenListe_Overflow(object sender, OverflowMetaInfo e)
        {   
            MessageBox.Show($"Limit von {e.MaxLimit} wurde überschritten!\n{e.Item} konnte nicht hinzugefügt werden.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _personenListe.Add(textBox1.Text);
        }
    }
}
