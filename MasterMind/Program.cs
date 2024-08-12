using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind
{
    class Program
    {
        static void Main(string[] args)
        {
            var mind = new MasterMindEngine();

            Console.WriteLine("Welcome to the MasterMind bot!");
            Console.WriteLine("Write down on a piece of paper your secret code consisting of four colors.");
            Console.WriteLine("The colors are White, B (Black), R (Red), G (Green), U (Blue) and Y (Yellow). Each color must be used only once.");
            Console.WriteLine("A legal code could be 'BRGW'.");
            Console.WriteLine("Whenever I make a guess you have to answer with 2-4 letters:");
            Console.WriteLine("  write B (black) if one of my guesses is at the correct place.)");
            Console.WriteLine("  write W (white) if the pin is correct but in a wrong place.");
            Console.WriteLine("So an example of an answer could be BBW  or it could be WWWW or even just WW.");

            string q;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Press enter to start a new game or type Q to quit. (you can type Q at any time to quit the game)");
                q = Console.ReadLine().ToUpper().Trim();
                if (q == "Q")
                {
                    break;
                }

                var cnt = 0;
                bool finished = false;
                bool isLegal;
                mind.Reset();
                Console.WriteLine("================");

                do
                {
                    cnt++;
                    var guess = mind.Guess();
                    isLegal = false;

                    while (!isLegal)
                    {
                        Console.WriteLine($"My {cnt}. guess is {guess}. What is your answer?");
                        q = Console.ReadLine().ToUpper();
                        if (q == "Q")
                        {
                            break;
                        }

                        isLegal = mind.Answer(q, guess, out finished);
                        if (!isLegal)
                        {
                            if (!finished)
                            {
                                Console.WriteLine($"That is not a legal answer.");
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine($"I don't know what to guess... I have no clue what you said. I think you might have made an incorrect answer at some point. Please start a new game.");
                                Console.WriteLine();
                            }
                        }
                    }
                }
                while (q != "Q" && !finished);

                if (finished && isLegal)
                {
                    Console.WriteLine($"Cool! I managed to guess your code in only {cnt} attempts!");
                    Console.WriteLine();
                }
            }
            while (q != "Q");

            Console.WriteLine("Ok, bye bye");
        }
    }
}
