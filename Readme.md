# For Audio data stream

https://github.com/naudio/NAudio

Open source library to manipulate audio streams

## Stream
##### Backing store: memory -> MemoryStream\
##### Backing store: file   -> FileStream\
##### Others:\
##### -> BufferedStream\
##### -> UnmanagedMemoryStream\
##### -> Compression.GZipStream\
##### ... and many others 
           

## Binary reader and writer
Cuando trabajamos con datos binarios, podemos usar
BinaryReader y BinaryWriter, heredan de System.Object
For binary files

## TextReaders
Cuando trabajamos con datos de texto podemos usar 
TextReader class y TextWriter class.

=========================================================

StreamReader y StringReader heredan de TextReader

StreamReader: \
Lee caracteres de un byte stream usando un encoding.\
string ReadLine();\
string ReadToEnd();

StringReader:
Lee de un objecto cadena.\
int Read();\
string ReadLine();\
string ReadToEnd();

=========================================================

StreamWriter y StringWriter heredan de TextWriter\

StreamWriter:
Escribe caracteres a una stream usando un encoding.
Write(bool);\
Write(decimal);\
Write(int);\
etc.

StringWriter:
Escribe a una cadena (StringBuilder por detras).\
Write(bool);\
Write(decimal);\
Write(int);\
Write(char);\
etc.

=========================================================

## To work with dynamic

Go to Pluralsight:
https://app.pluralsight.com/library/courses/c-sharp-dynamic-fundamentals/table-of-contents
