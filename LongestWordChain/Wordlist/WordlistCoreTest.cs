using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Wordlist
{
    
    class WordlistCoreTest
    {
        /*
        [DllImport(@"F:\\我爱学习学习爱我\\SoftwareEngineeringCourse\\LongestWordChain\\LongestWordChain\\LongestWordChain\\zbw\\Core.dll",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            EntryPoint = "gen_chain_word",
            ExactSpelling = false,
            SetLastError = true)]
        private static extern int gen_chain_word(string[] words, int len, string[] result, char head, char tail, bool enable_loop);
        */

        static void Main(string[] args)
        {
            

            for(int i = 1;  i <= 9; i++)
            {
                
                string FilePath = @"F:\我爱学习学习爱我\SoftwareEngineeringCourse\LongestWordChain\LongestWordChain\LongestWordChain\InputTest\input";
                FilePath += i.ToString() + ".txt";
                
                FileReader Reader = new FileReader(FilePath);
                Reader.ReadFile();
                string[] InputData = Reader.FileData;
                if (InputData != null)
                {
                    Console.WriteLine("Test "+ i.ToString() + " input:");
                    foreach (string s in InputData)
                    {
                        Console.Write(s + " ");
                    }
                    Console.WriteLine("");

                    Char start = '\0';
                    Char end = '\0';

                    switch (i)
                    {
                        case 1:
                            start = 'a';
                            break;
                        case 2:
                            start = 'z';
                            break;
                        case 3:
                            end = 'd';
                            break;
                        case 4:
                            end = 'z';
                            break;
                        case 5:
                            start = 'a';
                            end = 'd';
                            break;
                        case 6:
                            end = 'y';
                            start = 'x';
                            break;
                        case 7:
                            start = 'z';
                            break;
                        case 8:
                            start = 'd';
                            break;
                        case 9:
                            start = 'z';
                            start = 'd';
                            break;
                        default:
                            break;

                    }
                    string[] result = new string[10000];
                    int ReturnCode = CoreInterface.gen_chain_word(InputData, InputData.Length, result,start, end, true);
                    
                    Console.WriteLine("Result as following:");
                    foreach (string s in result)
                    {
                        Console.Write(s + " ");
                        if (s == null)
                        {
                            break;
                        }
                    }
                }
                Console.WriteLine("-----------------------------");
            }
            

            Console.Read();
        }
        
    }
}
