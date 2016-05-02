using System;
using System.IO;

namespace Zharkova.Nsudotnet.Enigma
{
    public enum Mode
    {
        Encrypt,
        Decrypt
    };
    class Parser
    {
        public const string EncryptCmd = "encrypt";
        public const string DecryptCmd = "decrypt";
        private readonly Mode _mode;
        private readonly string _inFile;
        private readonly string _outFile;
        private readonly string _keyFile;
        private readonly string _alg;

        public Parser(string[] agrs)
        {
            if (agrs.Length != 4 && agrs.Length != 5)
            {
                throw new ParserException("Not enough arguments.");
            }

            switch (agrs[0])
            {
                case EncryptCmd:
                    _mode = Mode.Encrypt;
                    break;
                case DecryptCmd:
                    _mode = Mode.Decrypt;
                    break;
                default:
                    throw new ParserException(string.Concat("Unknown mode: ", agrs[0]));
            }
            if (_mode == Mode.Encrypt)
            {
                for (var i = 1; i < agrs.Length; i++)
                {
                    switch (i)
                    {
                        case 1:
                            _inFile = ParseFileName(agrs[1], ".txt");
                            break;
                        case 2:
                            _alg = agrs[2];
                            break;
                        case 3:
                            _outFile = agrs[3];
                            break;
                        default:
                            throw new ParserException("Uncorrect agruments");
                    }
                }
            }
            if (_mode == Mode.Decrypt)
            {
                for (var i = 1; i < agrs.Length; i++)
                {
                    switch (i)
                    {
                        case 1:
                            _inFile = ParseFileName(agrs[1], ".bin");
                            break;
                        case 2:
                            _alg = agrs[2];
                            break;
                        case 3:
                            _keyFile = ParseFileName(agrs[3], ".key.txt");
                            break;
                        case 4:
                            _outFile = agrs[4];
                            break;
                        default:
                            throw new ParserException("Uncorrect agruments");
                    }
                }
            }

        }

        private string ParseFileName(string fileName, string extension)
        {
            if (!File.Exists(fileName))
            {
                throw new ParserException(string.Concat("File not exist: ", fileName));
            }
            if (!fileName.EndsWith(extension))
            {
                throw new ParserException(string.Concat("Wrong extension: ", fileName));
            }
            return fileName;
        }
       

        public string InFile
        {
            get { return _inFile;}
        }

        public string OutFile
        {
            get { return _outFile; }
        }

        public string KeyFile
        {
            get { return _keyFile; }
        }

        public string Algorithm
        {
            get { return _alg; }
        }

        public Mode Modifier
        {
            get { return _mode; }
        }
    }

    public class ParserException : Exception
    {
        public ParserException(string msg) : base(string.Concat(msg)) { }
    }
}
