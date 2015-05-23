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
        private SymmetricAlgorithm _algorithm;
        private String _key;
        private String _IV;

        private void SetAlgorithm(String name)
        {
            switch (name)
            {
                case "aes":
                case "AES":
                    {
                        _algorithm = new AesCryptoServiceProvider();
                        break;
                    }
                case "des":
                case "DES":
                    {
                        _algorithm = new DESCryptoServiceProvider();
                        break;
                    }
                case "rc2":
                case "RC2":
                    {
                        _algorithm = new RC2CryptoServiceProvider();
                        break;
                    }
                case "rijndael":
                case "Rijndael":
                    {
                        _algorithm = new RijndaelManaged();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("I do not know this encryption algorithm :(\nPress any key...");
                        Environment.Exit(0);
                        break;
                    }
            }

        }

        public void EncryptOrDecrypt(String keyword, String inputFile, String nameAlgorithm, String outputFile, String nameKeyFile)
        {
            SetAlgorithm(nameAlgorithm);

            switch (keyword)
            {
                case "encrypt":
                    {
                        _algorithm.GenerateIV();
                        _algorithm.GenerateKey();

                        _IV = Convert.ToBase64String(_algorithm.IV);
                        _key = Convert.ToBase64String(_algorithm.Key);

                        using (var keyFile = File.Create(nameKeyFile))
                        {
                            using (var streamKey = new StreamWriter(keyFile))
                            {
                                streamKey.WriteLine(_IV);
                                streamKey.WriteLine(_key);
                            }
                        }

                        try
                        {
                            using (var input = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                            {
                                using (var result = File.Create(outputFile))
                                {
                                    using (var cryptoStream = new CryptoStream(result, _algorithm.CreateEncryptor(), CryptoStreamMode.Write))
                                    {
                                        input.CopyTo(cryptoStream);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Environment.Exit(0);
                        }
                        break;

                    }
                case "decrypt":
                    {
                        try
                        {
                            using (var keyFile = File.Open(nameKeyFile, FileMode.Open))
                            {
                                using (var readKey = new StreamReader(keyFile))
                                {
                                    _algorithm.IV = Convert.FromBase64String(readKey.ReadLine());
                                    _algorithm.Key = Convert.FromBase64String(readKey.ReadLine());
                                }
                            }

                            using (var output = new FileStream(outputFile, FileMode.Open, FileAccess.Read))
                            {
                                using (var result = File.Create(inputFile))
                                {
                                    using (var cryptoStream = new CryptoStream(output, _algorithm.CreateDecryptor(_algorithm.Key, _algorithm.IV), CryptoStreamMode.Read))
                                    {
                                        cryptoStream.CopyTo(result);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Environment.Exit(0);
                        }
                        break;
                    }
            }
        }
    }
}
