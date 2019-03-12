using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LongestWordChain
{
    /// <summary>
    /// The class that integrates all the other class and solve the problem
    /// </summary>

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
            int DllReturnCode = -1;
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
                        DllReturnCode = longestPathSolver.Solve();

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
                    DllReturnCode = longestPathSolver.Solve();
                    //Test file open with csharp
                    //FileStream F = new FileStream(InputFilePath,FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    //Console.WriteLine("File open successfully from csharp");
                    //F.Close();

                }

                //check return code to check if there is any file exception or finished normally
                switch (DllReturnCode)
                {
                    case 0:
                        Console.WriteLine("Dll core return normally");
                        return;
                    case 1:
                        //file open exception:
                        try
                        {
                            throw new FileOpenFailed();
                        }
                        catch (FileOpenFailed e )
                        {
                            Console.WriteLine(e.Message);
                        }
                        return;
                    case 2:
                        //file contains illegalk circle without -r command
                        try
                        {
                            throw new IllegalFileOnNonRCommand();
                        }
                        catch (IllegalFileOnNonRCommand e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        return;
                    default:
                        return;
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
            string InputFilePath = "F:\\InputTest\\CompressedInput.txt";
            string[] SomeTestInput = new string[] { "-c", "-r","-h", "g" , InputFilePath}; 
            WordListCommandLineSolver wordListCommandLineSolver = new WordListCommandLineSolver( SomeTestInput );
            wordListCommandLineSolver.Solve();
            Console.Read();
        }
    }
}