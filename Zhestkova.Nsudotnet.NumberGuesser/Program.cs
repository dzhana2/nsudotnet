using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberGuesser
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime open = DateTime.Now;
            Random random = new Random();
            int number = random.Next(100);
            int retryCounter = 0;
            int angryCounter = 0;
            String answer;
            int intAnswer = 0;
            String[] memoryArray = new String[1000];

            Console.WriteLine("Hello. I must know your name. Although what's the difference. You've got nothing. If you want me to close, enter  q  instead of the next response.");
            String userName = Console.ReadLine();
            String[] angryString = new String[4] { String.Format("{0}, your failure evokes such wonderful memories.\n", userName), String.Format("{0}, tests show that you are - a loser.\n", userName), String.Format("{0}, how can we fail here? It's a simple test.\n", userName), String.Format("{0}, you smell? So smell my disappointment in you.\n", userName) };
            Console.WriteLine("Okay, {0}, I want to play with you in the game. I put forth a number in the range [0..100]. Can you guess it?\nWrite youre answer", userName);

            do
            {
                if (angryCounter == 4)
                {
                    Console.Write(angryString[random.Next(4)]);
                    angryCounter = 0;
                }

                answer = Console.ReadLine();

                if (answer.Equals("q"))
                {
                    Console.WriteLine("I'm sorry that you're leaving. Sorry for being mocked you.\nPress any key...");
                    Console.Read();
                    Environment.Exit(0);

                }

                bool checkAnswer = int.TryParse(answer, out intAnswer);

                if (checkAnswer)
                {

                    angryCounter++;
                    retryCounter++;

                    if (number > intAnswer)
                    {
                        Console.WriteLine("Conceived number is greater. Try Again.");
                        memoryArray[retryCounter] = String.Format("{0}, greater", answer);
                    }
                    if (number < intAnswer)
                    {
                        Console.WriteLine("Conceived number is smaller. Try Again.");
                        memoryArray[retryCounter] = String.Format("{0}, smaller", answer);
                    }

                }
                else
                {
                    Console.WriteLine("But ... It's not a number. I'm very disappointed this situation. I'm not going to count it as an attempt. Try agayn.");
                    continue;
                }

            }
            while (number != intAnswer);

            DateTime close = DateTime.Now;
            TimeSpan interval = close - open;
            Console.WriteLine("Fine. It should be noted that I created this test only to test failed. And you coped in {0} steps. Here they:", retryCounter);
            for (int i = 0; i < retryCounter; i++)
            {
                Console.WriteLine("{0}", memoryArray[i]);
            }
            Console.WriteLine("{0} , this is the correct answer", number);
            Console.WriteLine("\nYou spent it {0} minutes of your life\nPress any key...", interval.Minutes);
            Console.Read();
        }
    }
}
