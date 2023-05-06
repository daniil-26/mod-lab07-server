using System;
using System.Threading;


namespace Lab07
{
    struct PoolRecord
    {
        public Thread thread;
        public bool inUse;
    }

    class Server
    {
        static int threadsNumber;
        static int averageTime;
        static bool consoleOutput;

        Random rnd = new Random();

        PoolRecord[] pool;
        object threadLock = new object();

        Statistics statistics;
        public int requestCount = 0, processedCount = 0, rejectedCount = 0;

        public Server(int n, int t, Statistics statistics, bool o = false)
        {
            threadsNumber = n;
            averageTime = t;
            this.statistics = statistics;
            consoleOutput = o;
            pool = new PoolRecord[threadsNumber];
        }

        void Answer(object obj)
        {
            if (obj is ProcEventArgs e)
            {
                Thread.Sleep(rnd.Next(2 * averageTime));
                if (consoleOutput)
                {
                    Console.WriteLine(" V  заявка {0} выполнена", e.id);
                }                
                pool[e.n].inUse = false;
            }
        }

        public void proc(object sender, ProcEventArgs e)
        {
            lock (threadLock)
            {
                statistics.dimension(pool);
                if (consoleOutput)
                {
                    Console.WriteLine("Заявка с номером: {0}", e.id);
                }
                requestCount++;
                for (int i = 0; i < threadsNumber; i++)
                {
                    if (!pool[i].inUse)
                    {
                        e.n = i;
                        pool[i].inUse = true;
                        pool[i].thread = new Thread(new ParameterizedThreadStart(Answer));
                        pool[i].thread.Start(e);
                        if (consoleOutput)
                        {
                            Console.WriteLine("Заявка {0} выполняется в потоке {1}", e.id, e.n);
                        }
                        processedCount++;
                        return;
                    }
                }
                if (consoleOutput)
                {
                    Console.WriteLine(" X  заявка {0} отклонена", e.id);
                }
                rejectedCount++;
            }
        }
    }
}
