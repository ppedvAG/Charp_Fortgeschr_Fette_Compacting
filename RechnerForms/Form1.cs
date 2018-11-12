using RechnerContracts;
using RechnerHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RechnerForms
{
    public partial class Form1 : Form
    {
        IParser _parser;
        IRechner _rechner;

        public Form1()
        {
            InitializeComponent();
            //Bootstrapping
            _parser = new Parser();
            _rechner = new FlexRechner();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string eingabe = textBox1.Text;
            MessageBox.Show($"Ergebnis ist: {_rechner.Rechne(_parser.Parse(eingabe))}");
        }
    }
}
