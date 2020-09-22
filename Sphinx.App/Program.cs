using System;

namespace Sphinx.App
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
