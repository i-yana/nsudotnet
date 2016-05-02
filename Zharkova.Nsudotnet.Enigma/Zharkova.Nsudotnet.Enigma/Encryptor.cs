using System;
using System.IO;
using System.Security.Cryptography;

namespace Zharkova.Nsudotnet.Enigma
{
    class Encryptor
    {
        private readonly string _inputFile;
        private readonly string _outputFile;
        private readonly string _alg;

        public Encryptor(string inputFile, string alg, string outputFile)
        {
            _inputFile = inputFile;
            _alg = alg;
            _outputFile = outputFile;
        }

        public void Encrypt()
        {
            var algorithm = CryptAlgorithmFactory.Instance(_alg);
            algorithm.GenerateKey();
            algorithm.GenerateIV();
            var outDirectory = Path.GetDirectoryName(Path.GetFullPath(_inputFile));
            var keyFileName = string.Concat(Path.GetFileNameWithoutExtension(_inputFile), ".key.txt");
            var keyFilePath = string.Concat(outDirectory, Path.DirectorySeparatorChar, keyFileName);

            using (var streamWriter = new StreamWriter(keyFilePath))
            {
                streamWriter.WriteLine(Convert.ToBase64String(algorithm.Key));
                streamWriter.WriteLine(Convert.ToBase64String(algorithm.IV));

                using (var inputStream = new FileStream(_inputFile, FileMode.Open))
                {
                    using (var ouputStream = new FileStream(_outputFile, FileMode.OpenOrCreate))
                    {
                        using (var cryptoStream = new CryptoStream(ouputStream, algorithm.CreateEncryptor(algorithm.Key, algorithm.IV),
                            CryptoStreamMode.Write))
                        {
                            inputStream.CopyTo(cryptoStream);
                        }
                    }
                }
            }
        }
    }
}
