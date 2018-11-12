using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTasksAsyncAwait
{
    class Program
    {

        static async Task Main(string[] args)
        {
            //Am besten immer nur maximal eine Methode pro Programmdurchlauf auskommentieren

            //EinfacherThread();
            //for (int i = 0; i < 10; i++)
            //{
            //    ThreadWettrennen();
            //}
            //ThreadsCanceln();
            //TaskTests();
            ParallelKlasse();
            //AsyncAwait();


            Console.WriteLine("Main-Thread ist fertig");
            Console.ReadKey();
            Console.WriteLine("Main-Thread wird geschlossen");

        }

        #region Async-Await
        private static void AsyncAwait()
        {
            List<string> URLs = new List<string>() {
                "http://www.google.de",
                "http://www.zeit.de",
                "http://www.gibtesbestimmtnicht.de",
                "http://www.cnn.com"
            };

            foreach (var item in URLs)
            {
                CheckeWebsite(item);
            }

        }

        private async static ValueTask CheckeWebsite(string url)
        {
            if (url == string.Empty)
                return;

            HttpClient client = new HttpClient();
            try
            {
                await client.GetStringAsync(url);
                Thread.Sleep(4000);
            }
            catch (Exception)
            {
                Console.WriteLine($"{url} ist falsch!");
                return;
            }
            Console.WriteLine($"{url} ist richtig!");
        }
        #endregion

        #region Parallel-Klasse


        public static readonly object dummy = new object();
        public static readonly object dummy2 = new object();

        private static void ParallelKlasse()
        {
            List<int> zahlenListe = new List<int>() { 2, 10, 8, 5, 4 };

            int summe = 0;

            Console.WriteLine("Normales ForEach:");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            foreach (var item in zahlenListe)
            {
                //Thread.Sleep(10);
                int result = item * item;
                Console.WriteLine(result);
                summe += result;
            }
            watch.Stop();
            Console.WriteLine("Summe: " + summe);
            Console.WriteLine($"Dauer: {watch.ElapsedMilliseconds}");
            watch.Reset();

            Console.WriteLine("\nParallel ForEach:");
            summe = 0;
            watch.Start();

            var loopresult = Parallel.ForEach(zahlenListe, (z, state) =>
            {
                Thread.Sleep(10);
                int result = z * z;
                Console.WriteLine(result);
                lock (dummy)
                {
                    summe += result;
                }
                if (z == 8)
                    state.Break();
            });
            watch.Stop();
            Console.WriteLine($"Dauer: {watch.ElapsedMilliseconds}");
            Console.WriteLine($"Lowest Break: {loopresult.LowestBreakIteration}");
            Console.WriteLine("Summe: " + summe);


            Console.WriteLine("\nSortierung der Liste");

            watch.Reset();
            watch.Start();
            //AsParallel lohnt sich erst bei einem sehr komplizierten OrderBy-Algorithmus, der mehr Zeit
            //beansprucht als der Overhead, der durch die Erstellung der Threads anfällt.
            zahlenListe.AsParallel().OrderBy(z => z).ToList().ForEach(r => Console.WriteLine(r));
            watch.Stop();
            Console.WriteLine($"Dauer AsParallel:  {watch.ElapsedMilliseconds}");

            watch.Reset();
            watch.Start();
            zahlenListe.OrderBy(z => z).ToList().ForEach(r => Console.WriteLine(r));
            watch.Stop();
            Console.WriteLine($"Dauer mit mit normalen OrderBy: {watch.ElapsedMilliseconds}");


        }
        #endregion

        #region Task-Klasse
        private static void TaskTests()
        {
            Task<string> schuhmacher = new Task<string>(() =>
            {
                Thread.Sleep(1);
                MacheEtwasAufwändiges("Schuhmacher", TimeSpan.FromMilliseconds(50), 50);
                return "Schuhmacher";
            });
            Task<string> häkkinen = new Task<string>(() =>
            {
                MacheEtwasAufwändiges("Häkkinen", TimeSpan.FromMilliseconds(50), 50, null, 1000);
                return "Häkkinen";
            });

            _cts = new CancellationTokenSource();

            schuhmacher.Start();
            häkkinen.Start();


            int siegerIndex = Task.WaitAny(new Task[] { schuhmacher, häkkinen }, 100);

            Console.WriteLine($"Index: {siegerIndex}");

            if (siegerIndex == -1)
            {
                Console.WriteLine("Keine Lust mehr");
                return;
            }

            string sieger = siegerIndex == 0 ? schuhmacher.Result : häkkinen.Result;

            Console.WriteLine($"Sieger: {sieger}");

            Task.WaitAll(schuhmacher, häkkinen);
        }
        #endregion



        #region Threads Canceln

        private static CancellationTokenSource _cts = new CancellationTokenSource();

        private static void ThreadsCanceln()
        {
            Thread t1 = new Thread(() => MacheEtwasAufwändiges("T1", TimeSpan.FromSeconds(1), 60));
            t1.Start();
            _cts = new CancellationTokenSource();
            ConsoleKey key = Console.ReadKey().Key;
            if (key == ConsoleKey.A)
            {
                //Hartes Beenden, nur im Notfall verwenden
                t1.Abort();
            }
            else if (key == ConsoleKey.B)
            {
                _cts.CancelAfter(2000);
            }

            t1.Join();
            Console.WriteLine("Fertig");
        }
        #endregion



        #region Thread-Wettrennen

        private static string siegerGlobal;

        private static void ThreadWettrennen()
        {
            siegerGlobal = string.Empty;
            Thread schuhmacher = new Thread(() =>
            {
                Thread.Sleep(1);
                MacheEtwasAufwändiges("Schuhmacher", TimeSpan.FromMilliseconds(50), 50);
            });
            Thread häkkinen = new Thread(() => { MacheEtwasAufwändiges("Häkkinen", TimeSpan.FromMilliseconds(50), 50); });


            schuhmacher.Start();
            häkkinen.Start();

            schuhmacher.Join();

            häkkinen.Join();

            Console.WriteLine($"Rennen ist beendet, Sieger ist: {siegerGlobal}");
        }
        #endregion


        #region Einfacher Thread

        private static void EinfacherThread()
        {
            string namedesThrad = "dasds";
            Thread t1 = new Thread(() =>
            {
                MacheEtwasAufwändiges(namedesThrad, TimeSpan.FromMilliseconds(100), 50,
                (string s) => { });
            });

            t1.IsBackground = false;
            t1.Priority = ThreadPriority.Highest;
            t1.Start();
            Console.WriteLine("Thread wurde gestartet");
            Console.WriteLine("Drücke Taste zum Beenden");
            Console.ReadLine();
        }
        #endregion

        private static void MacheEtwasAufwändiges(string name, TimeSpan sleepTime, int schritte, Action<string> callback = null, int timeout = 100000000)
        {
            try
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                for (int i = 0; i < schritte; i++)
                {
                    _cts.Token.ThrowIfCancellationRequested();
                    Thread.Sleep(sleepTime);
                    Console.WriteLine($"{name}: {i + 1}. Schritt fertig");

                    _cts.Token.ThrowIfCancellationRequested();

                    if (_cts.Token.IsCancellationRequested)
                    {
                        Console.WriteLine("Ich breche es ab");
                        return;
                    }
                    if (watch.ElapsedMilliseconds > timeout)
                    {
                        //Interpolated String $"..."
                        Console.WriteLine($"{name} braucht heute etwas länger");
                        watch.Stop();
                        watch.Reset();
                    }
                }

                //Thread-Sicherheit herstellen
                bool blockTaken = false;
                //TryEnter wartet maximal 5 Sekunden auf die Freigabe des Objekts.
                //Wurde das Objekt bis dahin freigegeben, setzt es die Variable lockTaken auf true
                Monitor.TryEnter(dummy, TimeSpan.FromSeconds(5), ref blockTaken);
                if(blockTaken)
                {
                    if (siegerGlobal == string.Empty)
                        siegerGlobal = name;
                    //Dummy Objekt wieder freigeben
                    Monitor.Exit(dummy);
                }
                else
                {
                    Console.WriteLine("Deadlock-Fehler ist aufgetreten!");
                }
               
                //Generiert automatisch eine Monitor.Enter und Monitor.Exit-Anweisung,
                //allerdings mit unendlichem Timeout
                //lock (dummy)
                //{
                //    if (siegerGlobal == string.Empty)
                //        siegerGlobal = name;
                //}

                if (_cts.Token.IsCancellationRequested)
                {
                    Console.WriteLine("Spätes Abbrechen");
                }

                callback?.Invoke(name);
            }
            catch (Exception exp)
            {

                Console.WriteLine($"Ich wurde abortet!, Ursache ist {exp.Message}");
            }

        }
    }
}