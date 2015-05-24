using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Enigma
{
    class Cryptographer
    {
        private String _key;
        private String _IV;

        private SymmetricAlgorithm SetAlgorithm(String name)
        {
            name = name.ToLower();
            switch (name)
            {
                case "aes":
                    {
                        return new AesCryptoServiceProvider();
                    }
                case "des":
                    {
                        return new DESCryptoServiceProvider();
                    }
                case "rc2":
                    {
                        return new RC2CryptoServiceProvider();
                    }
                case "rijndael":
                    {
                        return new RijndaelManaged();
                    }
                default:
                    {
                        throw new Exception("I do not know this encryption algorithm :(\nPress any key...");
                    }
            }

        }

        public void EncryptOrDecrypt(String keyword, String inputFile, String nameAlgorithm, String outputFile, String nameKeyFile)
        {
            try
            {
                using (var algorithm = SetAlgorithm(nameAlgorithm))
                {
                    switch (keyword)
                    {
                        case "encrypt":
                            {
                                algorithm.GenerateIV();
                                algorithm.GenerateKey();

                                _IV = Convert.ToBase64String(algorithm.IV);
                                _key = Convert.ToBase64String(algorithm.Key);

                                using (var keyFile = File.Create(nameKeyFile))
                                {
                                    using (var streamKey = new StreamWriter(keyFile))
                                    {
                                        streamKey.WriteLine(_IV);
                                        streamKey.WriteLine(_key);
                                    }
                                }
                                using (var input = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                                {
                                    using (var result = File.Create(outputFile))
                                    {
                                        using (var cryptoStream = new CryptoStream(result, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
                                        {
                                            input.CopyTo(cryptoStream);
                                        }
                                    }
                                }
                                break;

                            }
                        case "decrypt":
                            {
                                using (var keyFile = File.Open(nameKeyFile, FileMode.Open))
                                {
                                    using (var readKey = new StreamReader(keyFile))
                                    {
                                        algorithm.IV = Convert.FromBase64String(readKey.ReadLine());
                                        algorithm.Key = Convert.FromBase64String(readKey.ReadLine());
                                    }
                                }

                                using (var output = new FileStream(outputFile, FileMode.Open, FileAccess.Read))
                                {
                                    using (var result = File.Create(inputFile))
                                    {
                                        using (var cryptoStream = new CryptoStream(output, algorithm.CreateDecryptor(algorithm.Key, algorithm.IV), CryptoStreamMode.Read))
                                        {
                                            cryptoStream.CopyTo(result);
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
        }
    }
}
