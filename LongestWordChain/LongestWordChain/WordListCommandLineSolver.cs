using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestWordChain
{
    class WordListCommandLineSolver
    {
        private readonly string[] CommandLineArgInput;
        private CommandArgInputParser commandArgInputParser;
        private LongestPathSolver longestPathSolver;


        public WordListCommandLineSolver(string[] _CommandLineArgInput)
        {
            CommandLineArgInput = _CommandLineArgInput;
        }

        public void Solve()
        {
            commandArgInputParser = new CommandArgInputParser(CommandLineArgInput);
            commandArgInputParser.Parse();
            if (commandArgInputParser.CommandLegal)
            {
                if (commandArgInputParser.CommandDuplicated)
                {
                    Console.WriteLine("Input command settings has duplicate commands: "
                        + CommandArgInputParser.ConvertCommandListToString(commandArgInputParser.GetParsedCommandList()));
                    Console.WriteLine("Derived an non-duplicated version of input command:"
                        + CommandArgInputParser.ConvertCommandListToString(commandArgInputParser.GetParsedCommandList(true)));
                    Console.WriteLine("Enter 'y' to use the distinct command");
                    int UseDistinctInput = Console.Read();
                    if (UseDistinctInput == int.Parse("y"))
                    {
                        longestPathSolver = new LongestPathSolver(commandArgInputParser.GetParsedCommandList(true), commandArgInputParser.FilePath);
                        longestPathSolver.Solve();
                    }
                    else
                    {
                        Console.WriteLine("Exit : duplicated input command");
                        return;
                    }
                }
                else
                {
                    longestPathSolver = new LongestPathSolver(commandArgInputParser.GetParsedCommandList(), commandArgInputParser.FilePath);
                    longestPathSolver.Solve();
                }
            }
            else
            {
                Console.WriteLine("Exit : Input command illegal");
                return;
            }
        }


        static void Main(string[] args)
        {
            //WordListCommandLineSolver wordListCommandLineSolver = new WordListCommandLineSolver(args);
            //wordListCommandLineSolver.Solve();
        }
    }
}