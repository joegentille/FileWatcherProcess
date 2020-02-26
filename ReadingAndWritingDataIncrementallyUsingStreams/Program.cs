using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

namespace ReadingAndWritingDataIncrementallyUsingStreams
{
    class Program
    {
        private static ConcurrentDictionary<string, string> FilesToProcess = new ConcurrentDictionary<string, string>();

        static void Main(string[] args)
        {
            Console.WriteLine("Parsing command line options");

            var directoryToWatch = args[0];

            if (!Directory.Exists(directoryToWatch))
            {
                Console.WriteLine($"Error: {directoryToWatch} does not exist");
            }
            else
            {
                Console.WriteLine($"Watching directory {directoryToWatch} for changes");

                using (var inputFileWatcher = new FileSystemWatcher(directoryToWatch))
                using (var timer = new Timer(ProcessFiles, null, 0, 1000))
                {
                    inputFileWatcher.IncludeSubdirectories = false;
                    inputFileWatcher.InternalBufferSize = 32768; // 32 KB
                    inputFileWatcher.Filter = "*.*";
                    inputFileWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
                    //inputFileWatcher.NotifyFilter = NotifyFilters.FileName;

                    inputFileWatcher.Created += FileCreated;
                    inputFileWatcher.Changed += FileChanged;
                    inputFileWatcher.Deleted += FileDeleted;
                    inputFileWatcher.Renamed += FileRenamed;
                    inputFileWatcher.Error += WatcherError;

                    inputFileWatcher.EnableRaisingEvents = true;

                    Console.WriteLine("Press enter to quit");
                    Console.ReadLine();
                }
            }
        }

        private static void FileCreated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"* File created: {e.Name} - type: {e.ChangeType}");
            //var fileProcessor = new FileProcessor(e.FullPath);
            //fileProcessor.Process();

            FilesToProcess.TryAdd(e.FullPath, e.FullPath);
        }

        private static void FileChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"* File changed: {e.Name} - type: {e.ChangeType}");
            //var fileProcessor = new FileProcessor(e.FullPath);
            //fileProcessor.Process();

            FilesToProcess.TryAdd(e.FullPath, e.FullPath);
        }

        private static void FileDeleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"* File deleted: {e.Name} - type: {e.ChangeType}");
        }

        private static void FileRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"* File renamed: {e.OldName} to {e.Name} - type: {e.ChangeType}");
        }

        private static void WatcherError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine($"Error: file system watching may no longer be active: {e.GetException()}");
        }

        private static void ProcessSingleFile(string filePath)
        {
            var fileProcessor = new FileProcessor(filePath);
            fileProcessor.Process();
        }

        private static void ProcessDirectory(string directoryPath, string fileType)
        {
            //var allFiles = Directory.GetFiles(directoryPath);
            switch (fileType)
            {
                case "TEXT":
                    string[] textFiles = Directory.GetFiles(directoryPath, "*.txt");
                    foreach (var textFilePath in textFiles)
                    {
                        var fileProcessor = new FileProcessor(textFilePath);
                        fileProcessor.Process();
                    }
                    break;
                default:
                    Console.WriteLine($"Error: {fileType} is not supported");
                    return;
            }
        }

        private static void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        private static void ReadLine()
        {
            Console.ReadLine();
        }

        private static void ProcessFiles(Object stateInfo)
        {
            foreach (var filename in FilesToProcess.Keys) //May not be in order of adding
            {
                if (FilesToProcess.TryRemove(filename, out _))
                {
                    var fileProcessor = new FileProcessor(filename);
                    fileProcessor.Process();
                }
            }
        }

    }
}
