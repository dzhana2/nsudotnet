using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Resources;

namespace Enigma
{
    class Program
    {
        private static String _originalFile;
        private static String _binFile;
        private static String _nameAlgorithm;
        private static String _keyFile;
        private static String _keyword;
        private const string _nameForKeyFile = "file.key.txt";

        private static void ParseArgument(String[] array)
        {
            using (ResourceReader reader = new ResourceReader("Message.resources"))
            {
                bool flag = false;
                String message;
                byte[] byteMessage;

                if (0 == array.Length)
                {
                    reader.GetResourceData("notArgument", out message, out byteMessage);
                    Console.WriteLine(Encoding.ASCII.GetString(byteMessage));
                    Console.Read();
                    Environment.Exit(0);
                }

                _keyword = array[0].ToLower();

                if (_keyword.Equals("encrypt"))
                {
                    if (array.Length != 4)
                    {
                        reader.GetResourceData("wrongEncrypt", out message, out byteMessage);
                        Console.WriteLine(Encoding.ASCII.GetString(byteMessage));
                        Console.Read();
                        Environment.Exit(0);
                    }
                    else
                    {
                        _originalFile = array[1];
                        _nameAlgorithm = array[2];
                        _binFile = array[3];
                        _keyFile = _nameForKeyFile;
                        flag = true;
                    }
                }
                if (_keyword.Equals("decrypt"))
                {
                    if (array.Length != 5)
                    {
                        reader.GetResourceData("wrongDecrypt", out message, out byteMessage);
                        Console.WriteLine(Encoding.ASCII.GetString(byteMessage));
                        Console.Read();
                        Environment.Exit(0);
                    }
                    else
                    {
                        _binFile = array[1];
                        _nameAlgorithm = array[2];
                        _keyFile = array[3];
                        _originalFile = array[4];
                        flag = true;
                    }
                }
                if (!flag)
                {
                    reader.GetResourceData("wrongKeyword", out message, out byteMessage);
                    Console.WriteLine(Encoding.ASCII.GetString(byteMessage));
                    Console.Read();
                    Environment.Exit(0);
                }
            }
        }

        static void Main(string[] args)
        {
            ParseArgument(args);

            Cryptographer cryptographer = new Cryptographer();
            cryptographer.EncryptOrDecrypt(_keyword, _originalFile, _nameAlgorithm, _binFile, _keyFile);
        }
    }
}
