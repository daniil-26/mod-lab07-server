using System;
using System.Threading;


namespace Lab07
{
    class Client
    {
        Server server;
        Statistics statistics;

        Random rnd = new Random();
        public int averageTime;

        public delegate void EventHandler<ProcEventArgs>(Client client, ProcEventArgs e);
        public event EventHandler<ProcEventArgs> request;

        public Client(Server server, int averageTime, Statistics statistics)
        {
            this.server = server;
            this.averageTime = averageTime;
            this.request += server.proc;
            this.statistics = statistics;
        }

        public void createRequests(int n)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(Requests));
            thread.Start(n);
        }

        private void Requests(object obj)
        {
            if (obj is int n)
            {
                for (int id = 0; id < n; id++)
                {
                    OnProc(new ProcEventArgs(id));
                    Thread.Sleep(rnd.Next(2 * averageTime));
                }
                statistics.total("results.txt", this, server);
            }
        }

        protected virtual void OnProc(ProcEventArgs e)
        {
            EventHandler<ProcEventArgs> handler = request;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
