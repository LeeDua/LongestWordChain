using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Wordlist
{
    class Wordlist
    {
       
        static void Main(string[] args)
        {
            //string[] mArgs = new string[] { "-w", "-r", @"..\..\..\InputTest\input.txt" };
            //WordListCommandLineSolver.CoreCommandLineSolver(mArgs);

            WordListCommandLineSolver.CoreCommandLineSolver(args);

        }

    }
}
