using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReadingAndWritingDataIncrementallyUsingStreams
{
    public class TextFileProcessor
    {
        public string InputFilePath { get; }
        public string OutputFilePath { get; }

        public TextFileProcessor(string inputFilePath, string outputFilePath)
        {
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
        }

        // Using FileStream
        //public void Process()
        //{
        //    using (var inputFileStream = new FileStream(InputFilePath, FileMode.Open))
        //    using (var inputStreamReader = new StreamReader(inputFileStream))
        //    using (var outputtFileStream = new FileStream(OutputFilePath, FileMode.Create))
        //    using (var outputStreamWrite = new StreamWriter(outputtFileStream))
        //    {
        //        while(!inputStreamReader.EndOfStream)
        //        {
        //            string line = inputStreamReader.ReadLine();
        //            string processedLine = line.ToUpperInvariant();
        //            outputStreamWrite.WriteLine(processedLine);
        //        }
        //    }
        //}

        // Using StreamReader
        //public void Process()
        //{
        //    //using (var inputStreamReader = File.OpenText(InputFilePath))
        //    using (var inputStreamReader = new StreamReader(InputFilePath))            
        //    using (var outputStreamWrite = new StreamWriter(OutputFilePath)) //Behind the escenes it will create the FileStream for us.
        //    {
        //        while (!inputStreamReader.EndOfStream)
        //        {
        //            string line = inputStreamReader.ReadLine();
        //            string processedLine = line.ToUpperInvariant();
        //            bool isLastLine = inputStreamReader.EndOfStream;
        //            if(isLastLine)
        //            {
        //                outputStreamWrite.Write(processedLine);
        //            }
        //            else
        //            {
        //                outputStreamWrite.WriteLine(processedLine);
        //            }                    
        //        }
        //    }
        //}

        // Processing part of Stream
        public void Process()
        {
            //using (var inputStreamReader = File.OpenText(InputFilePath))
            using (var inputStreamReader = new StreamReader(InputFilePath))
            using (var outputStreamWrite = new StreamWriter(OutputFilePath)) //Behind the escenes it will create the FileStream for us.
            {
                var currentLineNumber = 1;
                while(!inputStreamReader.EndOfStream)
                {
                    string line = inputStreamReader.ReadLine();
                    if(currentLineNumber == 2)
                    {
                        Write(line.ToUpperInvariant());
                    }
                    else
                    {
                        Write(line);
                    }

                    currentLineNumber++;

                    void Write(string content)
                    {
                        bool isLastLine = inputStreamReader.EndOfStream;
                        if(isLastLine)
                        {
                            outputStreamWrite.Write(content);
                        }
                        else
                        {
                            outputStreamWrite.WriteLine(content);
                        }
                    }
                        
                }
            }
        }

    }
}
