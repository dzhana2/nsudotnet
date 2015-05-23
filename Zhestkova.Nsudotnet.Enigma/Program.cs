using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    class Program
    {
        private static String _originalFile;
        private static String _binFile;
        private static String _nameAlgorithm;
        private static String _keyFile;
        private static String _keyword;
        private const String _nameForKeyFile = "file.key.txt";

        private static void ParseArgument(String[] array)
        {

            bool flag = false;

            if (0 == array.Length)
            {
                Console.WriteLine("But then he must have arguments.\nPress any key...");
                Console.Read();
                Environment.Exit(0);
            }

            _keyword = array[0];

            if (_keyword.Equals("encrypt"))
            {
                if (array.Length != 4)
                {
                    Console.WriteLine("To encrypt, you must enter 3 paramters: the name of the file that are going to encrypt, encryption algorithm name (aes, des, rc2 or rijndael) and the name of the output file (*.bin).\nPress any key...");
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
                    Console.WriteLine("To decrypt, you must enter the 4 parameters: the name of the file that you are going to decipher (*.bin), the name of the algorithm (aes, des, rc2 or rijndael), file name with a key (*.txt) and the name of the output file.\nPress any key...");
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
                Console.WriteLine("You have to say what you want: to encode or decode the information. Use keywords: encryp or decrypt.\nTo encrypt, you must enter 3 paramters: the name of the file that are going to encrypt, encryption algorithm name (aes, des, rc2 or rijndael) and the name of the output file (*.bin).\nTo decrypt, you must enter the 4 parameters: the name of the file that you are going to decipher (*.bin), the name of the algorithm (aes, des, rc2 or rijndael), file name with a key (*.txt) and the name of the output file.\nPress any key...");
                Console.Read();
                Environment.Exit(0);
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
