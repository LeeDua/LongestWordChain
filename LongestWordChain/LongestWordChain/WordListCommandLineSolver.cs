using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestWordChain
{
    class WordListCommandLineSolver
    {
        public WordListCommandLineSolver()
        {

        }


        static void Main(string[] args)
        {
            try
            {
                throw new IllegalCommandKeyWord();
            }
            catch (IllegalCommandKeyWord e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }
        }
    }
}