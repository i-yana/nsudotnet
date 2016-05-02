using System;
using System.Security.Cryptography;

namespace Zharkova.Nsudotnet.Enigma
{
    class CryptAlgorithmFactory
    {
        public const string Rc2 = "rc2";
        public const string Aes = "aes";
        public const string Des = "des";
        public const string Rijndael = "rijndael";

        private CryptAlgorithmFactory()
        {
        }

        internal static SymmetricAlgorithm Instance(string alg)
        {
            switch (alg)
            {
                case Rc2:
                    return new RC2CryptoServiceProvider();
                case Aes:
                    return new AesCryptoServiceProvider();
                case Des:
                    return new DESCryptoServiceProvider();
                case Rijndael:
                    return new RijndaelManaged();
                default:
                    throw new AlgorithmNotFoundException(alg);
            }
        }
    }

    public class AlgorithmNotFoundException : Exception
    {
        public AlgorithmNotFoundException(string msg) : base(string.Concat("Algorithm not found: ",msg)) {}
    }
}
