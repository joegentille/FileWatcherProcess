using System;
using System.IO;

namespace ReadingAndWritingEntireFilesIntoMemory
{
    public class FileProcessor
    {
        private string InputFilePath { get; }
        private static readonly string BackupDirectoryName = "backup";
        private static readonly string InProgressDirectoryName = "processing";
        private static readonly string CompleteDirectoryName = "complete";

        public FileProcessor(string filePath)
        {
            this.InputFilePath = filePath;
        }

        public void Process()
        {
            Console.WriteLine($"Begin process of {InputFilePath}");
            if (!File.Exists(InputFilePath))
            {
                Console.WriteLine($"Error: file {InputFilePath} does not exist.");
                return;
            }

            string rootDirectoryPath = new DirectoryInfo(InputFilePath).Parent.Parent.FullName;
            Console.WriteLine($"Root data path is {rootDirectoryPath} ");

            //Check if backup dir exists
            string inputFileDirectoryPath = Path.GetDirectoryName(InputFilePath);
            string backupDirectoryPath = Path.Combine(rootDirectoryPath, BackupDirectoryName);

            if (!Directory.Exists(backupDirectoryPath))
            {
                Console.WriteLine($"Creating {backupDirectoryPath}");
                Directory.CreateDirectory(backupDirectoryPath);
            }

            //Copy file to backup dir
            string inputFileName = Path.GetFileName(InputFilePath);
            string backupFilePath = Path.Combine(backupDirectoryPath, inputFileName);
            Console.WriteLine($"Copying {InputFilePath} to {backupFilePath}");
            File.Copy(InputFilePath, backupFilePath, true);

            //Move to in progress dir
            Directory.CreateDirectory(Path.Combine(rootDirectoryPath, InProgressDirectoryName));
            string inProgressFilePath = Path.Combine(rootDirectoryPath, InProgressDirectoryName, inputFileName);

            if (File.Exists(inProgressFilePath))
            {
                Console.WriteLine($"Error: a file with the name {inProgressFilePath} is already being processed");
                return;
            }
            Console.WriteLine($"Moving {InputFilePath} to {inProgressFilePath}");
            File.Move(InputFilePath, inProgressFilePath);

            string completedDirectoryPath = Path.Combine(rootDirectoryPath, CompleteDirectoryName);
            Directory.CreateDirectory(completedDirectoryPath);

            string extension = Path.GetExtension(InputFilePath);
            var completedFileName = $"{Path.GetFileNameWithoutExtension(InputFilePath)}-{Guid.NewGuid()}{extension}";
            var completedFilePath = Path.Combine(completedDirectoryPath, completedFileName);
            
            //Determine type of file
            switch (extension)
            {
                case ".txt":
                    var textProcessor = new TextFileProcessor(inProgressFilePath, completedFilePath);
                    textProcessor.Process();
                    break;
                case ".data":
                    var binaryProcessor = new BinaryFileProcessor(inProgressFilePath, completedFilePath);
                    break;
                default:
                    Console.WriteLine($"{extension} is an unsupported file type");
                    break;
            }

            Console.WriteLine($"Completed processing of {inProgressFilePath}");

            Console.WriteLine($"Deleting {inProgressFilePath}");
            File.Delete(inProgressFilePath);

        }
    }
}
