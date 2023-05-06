using System;
using System.IO;


namespace Lab07
{
    class Statistics
    {
        int downtimeNumber = 0;
        int busyThreadsNumber = 0;

        public void dimension(PoolRecord[] pool)
        {
            int useNumber = 0;
            foreach(PoolRecord record in pool)
            {
                if(record.inUse)
                {
                    useNumber++;
                }
            }
            busyThreadsNumber += useNumber;
            if (pool.Length - useNumber <= 1)
            {
                downtimeNumber++;
            }
        }

        public void total(string fname, Client client, Server server)
        {
            float systemDowntimeProbability = (float)downtimeNumber / server.requestCount;
            float systemFailureProbability = (float)server.rejectedCount / server.requestCount;
            float relativeBandwidth = (float)server.processedCount / server.requestCount;
            float absoluteBandwidth = (float)(relativeBandwidth / client.averageTime * 1000);
            float averageBusyThreads = (float)busyThreadsNumber / server.requestCount;

            StreamWriter writer = new StreamWriter(fname);
            writer.Write("Практические данные\n\n" +
                "Вероятность простоя системы: {0}\n" +
                "Вероятность отказа системы: {1}\n" +
                "Относительная пропускная способность: {2}\n" +
                "Абсолютная пропускная способность: {3}\n" +
                "Среднее число занятых каналов: {4}\n",
                systemDowntimeProbability,
                systemFailureProbability,
                relativeBandwidth,
                absoluteBandwidth,
                averageBusyThreads);
            writer.Close();
        }
    }
}
