using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpecialTimer
{
    public partial class Form1 : Form
    {
        int _countdown = 60;


        public int Countdown
        {
            get
            {
                return _countdown;
            }
            set
            {
                _countdown = value;
                label1.Text = $"Countdown: {_countdown}";
            }
        }

        TimerAdvanced myTimer;

        public Form1()
        {
            InitializeComponent();

            myTimer = new TimerAdvanced(1000, () =>
            {

                bool nutzeBeginInvoke = false;


                if (nutzeBeginInvoke)
                {

                    //Variante mit BeginInvoke und EndInvoke

                    //Methode wird in parallelen Thread ausgeführt
                    IAsyncResult result = BeginInvoke(new Func<bool>
                        (
                            () =>
                            {
                                Countdown--;
                                Thread.Sleep(1000);
                                return true;
                            }
                        )
                    );

                    myTimer.Pausieren();
                    MessageBox.Show("Drücke Enter zum Fortfahren");

                    //Mit EndInvoke auf Ergebnis von BeginInvoke warten
                    if ((bool)EndInvoke(result))
                    {
                        myTimer.Fortsetzen();
                    }
                    myTimer.Fortsetzen();
                }
                else
                {
                    //Anweisung hat Auswirkungen auf den GUI-Thread und muss daher von diesem aus ausgeführt werden per Invoke
                    //Im Gegensatz zu BeginInvoke wartet die Programmausführung auf das das Ende der Invoke-Methode
                    InvokeAction(() => Countdown--);
                }

                if (Countdown == 0)
                {
                    myTimer.Stop();
                }
            }, 60);
        }

        private void InvokeAction(Action action)
        {
            this.Invoke(action);
        }

        private void countdownTimer_Tick(object sender, EventArgs e)
        {

            Countdown--;
            label1.Text = $"Countdown: {Countdown}";
            if (Countdown == 0)
            {
                countdownTimer.Stop();
            }
        }

        private void buttonStart_WindowsTimer_Click(object sender, EventArgs e)
        {
            countdownTimer.Start();
        }

        private void buttonStart_MyTimer_Click(object sender, EventArgs e)
        {
            myTimer.Start();
        }

        private void buttonStop_MyTimer_Click(object sender, EventArgs e)
        {
            Countdown = 60;
            myTimer.Stop();
        }

        private void buttonPause_MyTimer_Click(object sender, EventArgs e)
        {
            myTimer.Pausieren();
        }

        private void buttonResume_MyTimer_Click(object sender, EventArgs e)
        {
            myTimer.Fortsetzen();
        }
    }
}
