using System;
using System.IO;
using System.Security.Cryptography;

namespace Zharkova.Nsudotnet.Enigma
{
    class Decryptor
    {
        private string _inputFile;
        private readonly string _alg;
        private readonly string _keyFile;
        private string _outputFile;

        public Decryptor(string inputFile, string alg, string keyFile, string outputFile)
        {
            _inputFile = inputFile;
            _alg = alg;
            _keyFile = keyFile;
            _outputFile = outputFile;
        }

        public void Decrypt()
        {
            var algorithm = CryptAlgorithmFactory.Instance(_alg);
            using (var sr = new StreamReader(_keyFile))
            {
                algorithm.Key = Convert.FromBase64String(sr.ReadLine());
                algorithm.IV = Convert.FromBase64String(sr.ReadLine());
                using (var inputStream = new FileStream(_inputFile, FileMode.Open))
                {
                    using (var outputStream = new FileStream(_outputFile, FileMode.OpenOrCreate))
                    {
                        using (var cryptoStream = new CryptoStream(inputStream,algorithm.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            cryptoStream.CopyTo(outputStream);
                        }
                    }
                }
            }
        }
    }
}
