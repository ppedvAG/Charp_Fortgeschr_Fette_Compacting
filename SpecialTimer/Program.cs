using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpecialTimer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //Thread t1 = new Thread(() =>
            //{
            //    Thread.Sleep(5000);
            //    MessageBox.Show("Achtung, ihr System ist infiziert");
            //});
            //t1.Start();

            //Thread.Sleep(5000);
            //MessageBox.Show("Achtung, ihr System ist infiziert");

        }
    }
}
