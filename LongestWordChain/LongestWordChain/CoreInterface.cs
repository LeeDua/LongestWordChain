using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CoreInterface
    {
        [DllImport(@"CoreBase.dll",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            EntryPoint = "gen_chain_cpp",
            ExactSpelling = false,
            SetLastError = true)]
         private static extern IntPtr gen_chain_cpp(string[] words, int len,string[] result, bool count_by_word, bool enable_loop, char head, char tail);


        private static int gen_chain(string[] words, int len,string[] result, char head, char tail, bool enable_loop, bool count_by_word, bool count_by_char)
        {
            List<string> InitialCommand = new List<string>();
            if (head != '\0')
            {
                InitialCommand.Add("-h");
                InitialCommand.Add(head.ToString());
            }

            if (tail != '\0')
            {
                InitialCommand.Add("-t");
                InitialCommand.Add(tail.ToString());
            }

            if (enable_loop)
            {
                InitialCommand.Add("-r");
            }

            if (count_by_char)
            {
                InitialCommand.Add("-c");
            }

            if (count_by_word)
            {
                InitialCommand.Add("-w");
            }
            InitialCommand.Add("FilePathTooken");
            CommandArgInputParser Parser = new CommandArgInputParser(InitialCommand.ToArray());
            Parser.Parse();

            if (Parser.CommandLegal)
            {
                int[] CppDllReturnCode = new int[2];
                Marshal.Copy(
                gen_chain_cpp(words, len, result, count_by_word, enable_loop, head, tail),CppDllReturnCode, 0, 2);
                switch (CppDllReturnCode[0])
                {
                    case 0:
                        //Console.WriteLine("Dll exit normally");
                        return CppDllReturnCode[1];
                    case 1:
                        try
                        {
                            throw new FileOpenFailed();
                        }
                        catch (FileOpenFailed e)
                        {
                            Console.WriteLine(e.Message);
                            return -1;
                        }
                    case 2:
                        try
                        {
                            throw new IllegalFileOnNonRCommand();
                        }
                        catch (IllegalFileOnNonRCommand e)
                        {
                            Console.WriteLine(e.Message);
                            return -2;
                        }
                    default:
                        Console.WriteLine("Exit : In default case, cppdll return an undefined code");
                        Console.WriteLine("Undefined return code :" + CppDllReturnCode[0].ToString() + " " + CppDllReturnCode[1].ToString());
                        return 0;
                }
            }
            else
            {
                Console.WriteLine("Exit : Input command illegal");
                return 0;
            }
        }

        public static int gen_chain_word(string[] words, int len, string[] result, char head, char tail, bool enable_loop)
        {
            
            int ReturnCode = gen_chain( words,len,result,head,tail,enable_loop,true,false);
            return ReturnCode;
        }

        public static int gen_chain_char(string[] words, int len, string[] result, char head, char tail, bool enable_loop)
        {
            int ReturnCode = gen_chain(words,len, result,head,tail,enable_loop,false,true);
            return ReturnCode;
        }


    }
}
