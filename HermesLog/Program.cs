using System;
using System.Text;

namespace HermesLog
{
    class Program
    {
        static void Main(string[] args)
        {
            QueueProcess queueProcess = new QueueProcess();
            queueProcess.Run();

            Console.WriteLine("Application exit.");
        }
    }
}
