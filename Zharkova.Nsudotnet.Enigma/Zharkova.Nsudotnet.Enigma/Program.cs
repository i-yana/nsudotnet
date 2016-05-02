using System;

namespace Zharkova.Nsudotnet.Enigma
{
    class Program
    {
        public const string EncryptCmd = "encrypt";
        public const string DecryptCmd = "decrypt";

        static void Main(string[] args)
        {
            try
            {
                var parser = new Parser(args);
                switch (parser.Modifier)
                {
                    case Mode.Encrypt:
                        new Encryptor(parser.InFile, parser.Algorithm, parser.OutFile).Encrypt();
                        break;
                    case Mode.Decrypt:
                        new Decryptor(parser.InFile, parser.Algorithm, parser.KeyFile, parser.OutFile).Decrypt();
                        break;
                }
            }
            catch (ParserException e)
            {
                Console.WriteLine(e.Message);
                PrintUsages();
            }
            catch (AlgorithmNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        private static void PrintUsages()
        {
            Console.WriteLine("For encrypt: crypto.exe encrypt file.txt rc2 output.bin");
            Console.WriteLine("For decrypt: crypto.exe decrypt output.bin rc2 file.key.txt file.txt");
            Console.ReadKey();
        }
    }
}
