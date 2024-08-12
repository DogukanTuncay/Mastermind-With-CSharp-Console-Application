using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind
{
    public class MasterMindEngine
    {
        private List<string> pool;
        private string letters = "WBRGUY";

        public void Reset()
        {
            this.pool = new List<string>();
            foreach (var a in this.letters.ToArray())
            {
                var option1 = a.ToString();
                foreach (var b in this.letters.ToArray().Where(q => q != a))
                {
                    var option2 = option1 + b.ToString();
                    foreach (var c in this.letters.ToArray().Where(q => q != a && q != b))
                    {
                        var option3 = option2 + c.ToString();
                        foreach (var d in this.letters.ToArray().Where(q => q != a && q != b && q != c))
                        {
                            this.pool.Add(option3 + d.ToString());
                        }
                    }
                }
            }

            Console.WriteLine(this.pool.Count);
        }

        public string Guess()
        {
            return this.pool[new Random().Next(0, this.pool.Count)];
        }

        public bool Answer(string answer, string myGuess, out bool finished)
        {
            finished = false;
            answer.Replace(" ", "");
            var test = answer.Replace("W", "").Replace("B", "");
            if (test.Length > 0 || answer.Length < 2 || answer.Length > 4)
            {
                return false;
            }

            if (answer == "BBBB")
            {
                finished = true;
                return true;
            }

            var bAnswer = answer.Replace("W", "");
            var wAnswer = answer.Replace("B", "");
            RemoveGuessesWithNotXCorrect(myGuess, bAnswer.Length, wAnswer.Length);
            Console.WriteLine($"Pool size {this.pool.Count}");

            if (this.pool.Count == 0)
            {
                finished = true;
                return false;
            }

            return true;
        }

        private void RemoveGuessesWithNotXCorrect(string guess, int b, int w)
        {
            foreach (var option in this.pool.ToArray())
            {
                var c = (option[0] == guess[0] ? 1 : 0) + (option[1] == guess[1] ? 1 : 0) +
                    (option[2] == guess[2] ? 1 : 0) + (option[3] == guess[3] ? 1 : 0);
                if (c != b)
                {
                    pool.Remove(option);
                }
            }
            foreach (var option in this.pool.ToArray())
            {
                var c = (option.Contains(guess[0]) && option[0] != guess[0] ? 1 : 0) + (option.Contains(guess[1]) && option[1] != guess[1] ? 1 : 0) +
                    (option.Contains(guess[2]) && option[2] != guess[2] ? 1 : 0) + (option.Contains(guess[3]) && option[3] != guess[3] ? 1 : 0);
                if (c != w)
                {
                    pool.Remove(option);
                }
            }
        }
    }
}
