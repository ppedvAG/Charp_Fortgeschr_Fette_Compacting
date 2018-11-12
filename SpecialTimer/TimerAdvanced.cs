using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SpecialTimer
{
    /// <summary>
    /// Nachbau des WindowsForms-Timers mit Zusatzfuntkionen
    /// </summary>
    public class TimerAdvanced
    {

        public delegate void TickMethodType();

        Thread _timerThread;
        public int Interval { get; private set; }
        public TickMethodType TickMethode { get; private set; }
        public int MaximaleSchritte { get; private set; }
        public bool ImHintergrund { get; private set; }
        public bool AutomatischesInvoke { get; private set; }

        //Hilfobjekt zum Canceln des Threads
        private CancellationTokenSource _cts;

        //Hilfsobjekt zum Pausieren des Threads
        ManualResetEvent _event;

        //Benötigt WindowsBase als Referenz
        private Dispatcher _dispatcher;

        int _schritte = 0;

        public TimerAdvanced(int interval, TickMethodType tickMethode, int maximaleSchritte = 0, bool imHintergrund = false, bool automatischesInvoke = false)
        {
            //Um ein automatisches Invoke zu realisieren, muss eine Referenz auf den Dispatcher des GUI-Threads gespeichert werden,
            //um dann im Hintergrund-Thread eine Referenz auf diesen Dispatcher zu erhalten
            if(automatischesInvoke)
                _dispatcher = Dispatcher.CurrentDispatcher;

            Interval = interval;
            TickMethode = tickMethode;
            MaximaleSchritte = maximaleSchritte;
            ImHintergrund = imHintergrund;
        }

        public void Start()
        {
            if (_timerThread == null || (_timerThread != null && _timerThread.ThreadState != ThreadState.Running))
            {
                _cts = new CancellationTokenSource();
                _event = new ManualResetEvent(true);
                _timerThread = new Thread(() =>
                {
                    do
                    {
                        if (_cts.Token.IsCancellationRequested)
                        {
                            break;
                        }
                        _event.WaitOne();
                        Thread.Sleep(Interval);
                        _event.WaitOne();
                        if (_cts.Token.IsCancellationRequested)
                        {
                            break;
                        }

                        if (AutomatischesInvoke)
                        {
                            _dispatcher.Invoke(() => { TickMethode?.Invoke(); });
                        }
                        else
                        {
                            TickMethode?.Invoke();
                        }

                        if (MaximaleSchritte > 0)
                            _schritte++;
                    } while (_schritte < MaximaleSchritte);
                });
                _timerThread.Start();
                _timerThread.IsBackground = true;
            }
        }

        public void Stop()
        {
            _cts.Cancel();
            _timerThread = null;
        }

        public void Pausieren()
        {
            _event?.Reset();
        }

        public void Fortsetzen()
        {
            _event?.Set();
        }

    }
}
