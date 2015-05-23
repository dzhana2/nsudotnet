using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    class Program
    {

        private static String[] _week = new String[7] { " Mo\t ", " Tu\t ", " We\t ", " Th\t ", " Fr\t ", " Sa\t ", " Su\t " };

        static void Main(string[] args)
        {
            DateTime date = new DateTime();
            DateTime today = DateTime.Today;

            Console.WriteLine("Hey! Please, enter date in normal format.");
            String stringDate = Console.ReadLine();

            if (!DateTime.TryParse(stringDate, out date))
            {
                Console.WriteLine("This is a wrong date format.\nPress any key.");
                Console.Read();
                Environment.Exit(0);
            }

            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < 7; i++)
            {
                Console.Write(_week[i]);
            }
            Console.Write("\n");
            int dayInMonth = DateTime.DaysInMonth(date.Year, date.Month);

            DateTime firstDate = new DateTime(date.Year, date.Month, 1);
            int count = (int)firstDate.DayOfWeek;
            if (count == 0)
            {
                count = 7;
            }

            for (int i = 1; i < count; i++)
            {
                Console.Write("  \t");
            }

            for (int i = 1; i <= DateTime.DaysInMonth(date.Year, date.Month); i++)
            {
                if ((i == (int)today.Day) && (today.Month.Equals(date.Month)) && (today.Year.Equals(date.Year)))
                {
                    Console.BackgroundColor = ConsoleColor.Gray;

                }
                else
                {
                    if (i == (int)date.Day)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;

                    }
                }
                Console.Write(" {0}\t", i);
                switch (count)
                {
                    case 5:
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        }
                    case 6:
                        {
                            dayInMonth--;
                            break;
                        }
                    case 7:
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("\n");
                            dayInMonth--;
                            count = 0;
                            break;
                        }
                }
                count++;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nThe number of weekdays {0}", dayInMonth);
            Console.Read();

        }
    }
}
