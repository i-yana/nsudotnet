using System;
using System.IO;


namespace Zharkova.Nsudotnet.LinesCounter
{
    public class LineCounter
    {
        private readonly string _extension;

        public LineCounter(string extension)
        {
            _extension = extension;
        }

        public void CountLines()
        {
            var files = Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*." + _extension, SearchOption.AllDirectories);
            var fileNumber = 0;
            var lineNumber = 0;
            foreach (var f in files)
            {
                var reader = new StreamReader(f);
                var count = 0;
                var isComment = false;
                var isOneLineComment = false;
                var isNewLine = true;
                while (!reader.EndOfStream)
                {
                    var ch = (char)reader.Read();
                    if (ch == '\r' || ch == ' ')
                    {
                        continue;
                    }

                    if (ch == '/' && !isOneLineComment)
                    {
                        ch = (char)reader.Read();
                        if (ch == 0)
                        {
                            break;
                        }

                        if (ch == '*')
                        {
                            isComment = true;
                            continue;

                        }
                        if (ch == '/' && !isComment)
                        {
                            isOneLineComment = true;
                            continue;
                        }
                    }

                    if (ch == '*' && isComment)
                    {
                        ch = (char)reader.Read();
                        if (ch == 0)
                        {
                            break;
                        }
                        if (ch == '/')
                        {
                            isComment = false;
                            continue;
                        }
                    }
                    if (ch == '\n')
                    {
                        isNewLine = true;
                        if (isOneLineComment)
                        {
                            isOneLineComment = false;
                        }
                        continue;
                    }

                    if (isNewLine && !isComment && !isOneLineComment)
                    {
                        count++;
                        isNewLine = false;
                    }
                }
                fileNumber++;
                lineNumber += count;
            }
            Console.WriteLine("{1} lines in {0} files", fileNumber, lineNumber);
        }
    }
}
