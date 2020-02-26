using System;
using System.Collections.Concurrent;
using System.IO;

namespace ReadingAndWritingCSVData
{
    class Program
    {
        private static ConcurrentDictionary<string, string> FilesToProcess = new ConcurrentDictionary<string, string>();
        static void Main(string[] args)
        {
            Console.WriteLine("Parsing command line options");

            var directoryToWatch = args[0];

            if(!Directory.Exists(directoryToWatch))
            {

            }





            Console.ReadLine();
        }
    }
}
