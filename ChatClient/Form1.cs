using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        //Nuget-Package Microsoft.AspNet.SignalR.Client
        IHubProxy _proxy;
        HubConnection _connection;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _connection = new HubConnection("http://localhost:8080");
               
                _proxy = _connection.CreateHubProxy("chatHub");
                _proxy.On<string>("ShowMessage", m => AddMessage(m));
                _connection.Start();
            }
            catch (Exception exp)
            {
                AddMessage(exp.Message);
                return;
            }
            AddMessage("Erfolgreich verbunden!");
        }



        public void AddMessage(string message)
        {
            if (this.InvokeRequired)
            {
                //Diese Anweisung wird evtl. von einem externen Thread ausgeführt und muss daher invoked werden
                this.Invoke(new Action(() => listBox1.Items.Add(message)));
                return;
            }
            listBox1.Items.Add(message);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _proxy.Invoke("SendeNachricht", textBox1.Text);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _connection.Dispose();
        }
    }
}