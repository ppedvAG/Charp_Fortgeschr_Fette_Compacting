using Microsoft.AspNet.SignalR;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }

   

    public class ChatHub : Hub
    {

        Mutex mutex = new Mutex();

        //Können vom Client aufgerufen werden
        public void SendeNachricht(string message)
        {
            //Anderer Prozess

            //Evtl. greifen mehrere Clients gleichzeitig auf die AddMessage Methode zu.
            //Mittels Mutex wird sichergestellt, das immer nur ein Thread die AddMessage-Methode gleichzeitig ausführt.
            mutex.WaitOne();
            Form1.Self.AddMessage(message);
            mutex.ReleaseMutex();
            //Show-Message Methode aller verbundenen Clients aufrufen
            Clients.All.ShowMessage(message);
        }

        public override Task OnConnected()
        {
            Form1.Self.AddMessage($"Client mit ID {Context.ConnectionId} hat sich verbunden");
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Form1.Self.AddMessage($"Client mit ID {Context.ConnectionId} hat sich abgemeldet");
            return base.OnDisconnected(stopCalled);
        }
    }
}
