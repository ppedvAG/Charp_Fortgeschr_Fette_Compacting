using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatServer
{
    public partial class Form1 : Form
    {
        IDisposable _server;

        public static Form1 Self;

        public Form1()
        {
            InitializeComponent();
            Self = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Nuget-Package  Microsoft.AspNet.SignalR.SelfHost
                _server = WebApp.Start<Startup>("http://localhost:8080");
            }
            catch (Exception exp)
            {
                AddMessage(exp.Message);
                return;
            }

            AddMessage("Server wurde erfolgreich gestartet!");
        }

        public void AddMessage(string message)
        {
            if(this.InvokeRequired)
            {
                //TODO: Mutex
                this.Invoke(new Action(() => listBox1.Items.Add(message)));
                return;
            }
            listBox1.Items.Add(message);
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _server.Dispose();
        }
    }
}