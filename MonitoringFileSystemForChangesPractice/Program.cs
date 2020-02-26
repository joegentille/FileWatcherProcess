using System;
using System.Collections.Concurrent;

namespace MonitoringFileSystemForChangesPractice
{
    class Program
    {

        private static ConcurrentDictionary<string, string> FilesToProcess = new ConcurrentDictionary<string, string>();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
