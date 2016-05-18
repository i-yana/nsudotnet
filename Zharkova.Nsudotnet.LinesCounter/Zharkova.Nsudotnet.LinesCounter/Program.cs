using System;
using System.Diagnostics;


namespace Zharkova.Nsudotnet.LinesCounter
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Not enough arguments. Usage: extension");
                Console.ReadKey();
                return;
            }
            var lineCounter = new LineCounter(args[0]);
            lineCounter.CountLines();
            Console.ReadKey();
        }
    }
}
