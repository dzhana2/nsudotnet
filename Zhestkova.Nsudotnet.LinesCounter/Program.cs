using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinesCounter
{
    class Program
    {
        private const String _oneLineComment = "//";
        private const String _openComment = "/*";
        private const String _closeComment = "*/";

        private static int GetAllLines(String fileName)
        {

            String str;
            bool flagComment = false;
            int result = 0;
            int startComment;
            int endComment;

            using (var file = new StreamReader(fileName))
            {
                do
                {
                    str = file.ReadLine();
                    if (!String.IsNullOrEmpty(str))
                    {
                        str.Trim(null);
                        startComment = str.IndexOf(_oneLineComment);
                        if (0 != startComment)
                        {
                            startComment = str.IndexOf(_openComment);
                            endComment = str.LastIndexOf(_closeComment);
                            if (flagComment)
                            {
                                if ((endComment != str.Length - 2) && (endComment != -1))
                                {
                                    result++;
                                }
                            }
                            else
                            {
                                if (startComment == 0)
                                {
                                    if (-1 == endComment)
                                    {
                                        flagComment = true;
                                    }
                                    else
                                    {
                                        if (str.Length - 2 != endComment)
                                        {
                                            result++;
                                        }
                                    }
                                }
                                else
                                {
                                    result++;
                                }
                            }
                        }
                    }
                }
                while (str != null);
            }

            return result;

        }

        static void Main(string[] args)
        {

            String fileExtension;

            try
            {
                fileExtension = args[0];
            }
            catch (Exception)
            {
                Console.WriteLine("Please, enter file extension for me");
                fileExtension = Console.ReadLine();
            }

            fileExtension = String.Format("*.{0}", fileExtension);
            String path = Directory.GetCurrentDirectory();
            String[] allFiles = Directory.GetFiles(path, fileExtension, SearchOption.AllDirectories);

            if (1 > allFiles.Length)
            {

                Console.WriteLine("Files with the extension {0} not found\nPress any key...", fileExtension);
                Console.Read();
                Environment.Exit(0);

            }

            int allLines = 0;

            foreach (String fileName in allFiles)
            {
                allLines += GetAllLines(fileName);
            }
            Console.WriteLine("The number of lines in all the files in all subdirectories = {0}\nPress any key...", allLines);
            Console.Read();
        }
    }
}