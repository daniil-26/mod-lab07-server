using System;


namespace Lab07
{
    class Program
    {
        static double intakeIntensity = 7;
        static double serviceIntensity = 5;
        static int threadsNumber = 3;
        static int requestsNumber = 1000;

        static void Main()
        {
            Statistics statistics = new Statistics();
            Server server = new Server(threadsNumber, (int)(1000 / serviceIntensity), statistics);
            Client client = new Client(server, (int)(1000 / intakeIntensity), statistics);
            client.createRequests(requestsNumber);
        }
    }
}
