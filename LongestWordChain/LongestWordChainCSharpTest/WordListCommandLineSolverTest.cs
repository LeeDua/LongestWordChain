using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LongestWordChainCSharpTest
{

    [TestClass]
    public class WordListCommandLineSolverTest
    {
        private bool CheckOutputLegal(string[] OutputChain, char StartChar = '\0', char EndChar = '\0')
        {
            bool flag = true;
            if (OutputChain.Length <= 1)
            {
                return false;
            }
            else
            {
                if (StartChar != '\0')
                {
                    if (OutputChain[0][0] != StartChar)
                    {
                        flag = false;
                    }
                }
                if (EndChar != '\0')
                {
                    if (OutputChain[OutputChain.Length - 1][OutputChain[OutputChain.Length - 1].Length - 1] != EndChar)
                    {
                        flag = false;
                    }
                }

                for (int i = 1; i < OutputChain.Length; i++)
                {
                    if (OutputChain[i][0] != OutputChain[i - 1][OutputChain[i - 1].Length - 1])
                    {
                        flag = false;
                    }
                }
                return flag;
            }
        }
    


        private string[] FileReader(string FilePath = @"somepath\CurrentOutputFolder\solution.txt")
        {
            var ListData = new List<string>();
            string[] FileData;
            FileStream fileStream;

            fileStream = new FileStream(FilePath,
                FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.ASCII))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    ListData.Add(line);
                }
            }
            FileData = ListData.ToArray();
            return FileData;

        }



        [DllImport(@"somepath\too\reach\CoreBase.dll",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            EntryPoint = "get_wordchain",
            ExactSpelling = false,
            SetLastError = true)]
        private static extern int get_wordchain(string[] args, int len);
        
        [TestMethod]
        public void Test_0()
        {
            string[] args = new string[] { "-w", "-r", "..\\..\\..\\InputTest\\input.txt" };

            char sc = '\0';
            char ec = '\0';
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-h")
                {
                    sc = char.Parse(args[i + 1]);
                }
                if (args[i] == "-t")
                {
                    ec = char.Parse(args[i + 1]);
                }
            }
            int ResultCount = get_wordchain(args, args.Length);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
        }


        [TestMethod]
        public void Test_1()
        {
            string[] args = new string[] { "-w", "-r", "-h", "a", "..\\..\\..\\InputTest\\input1.txt" };
            char sc = '\0';
            char ec = '\0';
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-h")
                {
                    sc = char.Parse(args[i + 1]);
                }
                if (args[i] == "-t")
                {
                    ec = char.Parse(args[i + 1]);
                }
            }
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));

        }


        [TestMethod]
        public void Test_2()
        {
            string[] args = new string[] { "-w", "-r", "-h", "z", "..\\..\\..\\InputTest\\input2.txt" };
            char sc = '\0';
            char ec = '\0';
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-h")
                {
                    sc = char.Parse(args[i + 1]);
                }
                if (args[i] == "-t")
                {
                    ec = char.Parse(args[i + 1]);
                }
            }
            int ResultCount = get_wordchain(args, args.Length);
            string[] solution = FileReader();
            //Assert.IsFalse(CheckOutputLegal(solution, sc, ec));
            WordListCommandLineSolver.CoreCommandLineSolver(args);
        }


        [TestMethod]
        public void Test_3()
        {
            string[] args = new string[] { "-w", "-r", "-t", "d", "..\\..\\..\\InputTest\\input3.txt" };
            char sc = '\0';
            char ec = '\0';
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-h")
                {
                    sc = char.Parse(args[i + 1]);
                }
                if (args[i] == "-t")
                {
                    ec = char.Parse(args[i + 1]);
                }
            }
            int ResultCount = get_wordchain(args, args.Length);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
            WordListCommandLineSolver.CoreCommandLineSolver(args);
        }


        [TestMethod]
        public void Test_4()
        {
            string[] args = new string[] { "-w", "-r", "-t", "z", "..\\..\\..\\InputTest\\input4.txt" };
            char sc = '\0';
            char ec = '\0';
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-h")
                {
                    sc = char.Parse(args[i + 1]);
                }
                if (args[i] == "-t")
                {
                    ec = char.Parse(args[i + 1]);
                }
            }
            int ResultCount = get_wordchain(args, args.Length);
            string[] solution = FileReader();
            //Assert.IsFalse(CheckOutputLegal(solution, sc, ec));
            WordListCommandLineSolver.CoreCommandLineSolver(args);
        }

        [TestMethod]
        public void Test_5()
        {
            string[] args = new string[] { "-w", "-r", "-h", "a", "-t", "d", "..\\..\\..\\InputTest\\input5.txt" };
            char sc = '\0';
            char ec = '\0';
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-h")
                {
                    sc = char.Parse(args[i + 1]);
                }
                if (args[i] == "-t")
                {
                    ec = char.Parse(args[i + 1]);
                }
            }
            int ResultCount = get_wordchain(args, args.Length);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
            WordListCommandLineSolver.CoreCommandLineSolver(args);
        }

        [TestMethod]
        public void Test_6()
        {
            string[] args = new string[] { "-w", "-r", "-t", "y", "-h", "x", "..\\..\\..\\InputTest\\input6.txt" };
            char sc = '\0';
            char ec = '\0';
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-h")
                {
                    sc = char.Parse(args[i + 1]);
                }
                if (args[i] == "-t")
                {
                    ec = char.Parse(args[i + 1]);
                }
            }
            int ResultCount = get_wordchain(args, args.Length);
            string[] solution = FileReader();
            //Assert.IsFalse(CheckOutputLegal(solution, sc, ec));
            WordListCommandLineSolver.CoreCommandLineSolver(args);
        }

        [TestMethod]
        public void Test_7()
        {
            string[] args = new string[] { "-w", "-r", "-h", "z", "..\\..\\..\\InputTest\\input7.txt" };
            char sc = '\0';
            char ec = '\0';
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-h")
                {
                    sc = char.Parse(args[i + 1]);
                }
                if (args[i] == "-t")
                {
                    ec = char.Parse(args[i + 1]);
                }
            }
            int ResultCount = get_wordchain(args, args.Length);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
            WordListCommandLineSolver.CoreCommandLineSolver(args);
        }

        [TestMethod]
        public void Test_8()
        {
            string[] args = new string[] { "-w", "-r", "-t", "d", "..\\..\\..\\InputTest\\input8.txt" };
            char sc = '\0';
            char ec = '\0';
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-h")
                {
                    sc = char.Parse(args[i + 1]);
                }
                if (args[i] == "-t")
                {
                    ec = char.Parse(args[i + 1]);
                }
            }
            int ResultCount = get_wordchain(args, args.Length);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
            WordListCommandLineSolver.CoreCommandLineSolver(args);
        }

        [TestMethod]
        public void Test_9()
        {
            string[] args = new string[] { "-w", "-r", "-h", "z", "-t", "d", "..\\..\\..\\InputTest\\input9.txt" };
            char sc = '\0';
            char ec = '\0';
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-h")
                {
                    sc = char.Parse(args[i + 1]);
                }
                if (args[i] == "-t")
                {
                    ec = char.Parse(args[i + 1]);
                }
            }
            int ResultCount = get_wordchain(args, args.Length);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
            WordListCommandLineSolver.CoreCommandLineSolver(args);
        }

        [TestMethod]
        public void DuplicateCommand1()
        {
            string[] args = new string[] { "-w", "-r", "-h", "z", "-h", "z", "..\\..\\..\\InputTest\\input9.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            
        }

        [TestMethod]
        public void DuplicateCommand2()
        {
            string[] args = new string[] { "-w", "-w", "-h", "z", "..\\..\\..\\InputTest\\input9.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
        }


        [TestMethod]
        public void DuplicateCommand3()
        {
            string[] args = new string[] { "-w", "-t", "z", "-t", "z", "..\\..\\..\\InputTest\\input9.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
        }

        [TestMethod]
        public void NotExistedFile()
        {
            string[] args = new string[] { "-w", "-h", "z", "..\\..\\..\\InputTest\\NotExistedFile.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
        }

        [TestMethod]
        public void CommandIllegal()
        {
            string[] args = new string[] { "-x", "..\\..\\..\\InputTest\\input.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
        }

        [TestMethod]
        public void CoreInterfaceTest()
        {
            for (int i = 1; i <= 9; i++)
            {
                string FilePath = @"..\..\..\InputTest\input";
                FilePath += i.ToString() + ".txt";

                string[] InputData = FileReader(FilePath);
                if (InputData != null)
                {
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
                            end = 'd';
                            break;
                        case 9:
                            start = 'z';
                            end = 'd';
                            break;
                        default:
                            break;

                    }
                    string[] result = new string[InputData.Length];
                    int ReturnCode_word = CoreInterface.gen_chain_word(InputData, InputData.Length, result, start, end, true);
                    string[] solution = FileReader();
                    //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
                    int ReturnCode_char = CoreInterface.gen_chain_char(InputData, InputData.Length, result, start, end, true);
                    solution = FileReader();
                    //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
                }
            }
        }
        [TestMethod]
        public void RTest()
        {
            string[] args = new string[] { "-w","-r", "..\\..\\..\\InputTest\\smallrinput.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
        }
        [TestMethod]
        public void RTest_Noncommand()
        {
            string[] args = new string[] { "-w", "..\\..\\..\\InputTest\\smallrinput.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
        }


        [TestMethod]
        public void RTest_1()
        {
            string[] args = new string[] { "-w","-r","-h","v","..\\..\\..\\InputTest\\input.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
        }

        [TestMethod]
        public void RTest_2()
        {
            string[] args = new string[] { "-w", "-r", "-t", "y", "..\\..\\..\\InputTest\\input.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
        }

        [TestMethod]
        public void RTest_3()
        {
            string[] args = new string[] { "-w", "-r", "..\\..\\..\\InputTest\\input_r2.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
        }
        [TestMethod]
        public void RTest_4()
        {
            string[] args = new string[] { "-w", "-r","-t","j", "..\\..\\..\\InputTest\\input_r2.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
        }

        [TestMethod]
        public void RTest_5()
        {
            string[] args = new string[] { "-w", "-r", "-h", "a", "..\\..\\..\\InputTest\\input_r2.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec)); 
        }
        [TestMethod]
        public void RTest_6()
        {
            string[] args = new string[] { "-w", "-r", "-h", "a","-t","z", "..\\..\\..\\InputTest\\input_r2.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
        }
        [TestMethod]
        public void RTest_7()
        {
            string[] args = new string[] { "-c", "-r", "..\\..\\..\\InputTest\\input_r1.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
        }
        [TestMethod]
        public void RTest_8()
        {
            string[] args = new string[] { "-c", "-r", "-t", "j", "..\\..\\..\\InputTest\\input_r1.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
        }

        [TestMethod]
        public void RTest_9()
        {
            string[] args = new string[] { "-c", "-r", "-h", "a", "..\\..\\..\\InputTest\\input_r1.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
        }
        [TestMethod]
        public void RTest_10()
        {
            string[] args = new string[] { "-c", "-r", "-h", "a", "-t", "z", "..\\..\\..\\InputTest\\input_r1.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec)); 
        }
        [TestMethod]
        public void RTest_11()
        {
            string[] args = new string[] { "-w", "-r", "-h", "s", "-t","w","..\\..\\..\\InputTest\\input_r2.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
        }
        [TestMethod]
        public void RTest_12()
        {
            string[] args = new string[] { "-c", "-r", "-h", "s", "-t", "w", "..\\..\\..\\InputTest\\input_r2.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec)); 
        }
        [TestMethod]
        public void RTest_13()
        {
            string[] args = new string[] { "-c", "-r", "-h", "s", "..\\..\\..\\InputTest\\input_r3.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
        }
        [TestMethod]
        public void RTest_14()
        {
            string[] args = new string[] { "-c", "-r", "-t", "w" ,"..\\..\\..\\InputTest\\input_r3.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
        }
        [TestMethod]
        public void RTest_15()
        {
            string[] args = new string[] { "-c", "-r", "..\\..\\..\\InputTest\\input_r3.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
        }
        [TestMethod]
        public void RTest_16()
        {
            string[] args = new string[] { "-c", "-r","-h","g", "..\\..\\..\\InputTest\\input_r3.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
        }
        [TestMethod]
        public void RTest_17()
        {
            string[] args = new string[] { "-c", "-r", "-h", "a", "..\\..\\..\\InputTest\\input_r4.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
        }
        [TestMethod]
        public void RTest_18()
        {
            string[] args = new string[] { "-c", "-r", "-h", "m", "..\\..\\..\\InputTest\\input_r4.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
        }
        [TestMethod]
        public void RTest_19()
        {
            string[] args = new string[] { "-c", "-r", "..\\..\\..\\InputTest\\input_r4.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec)); 
        }
        [TestMethod]
        public void RTest_20()
        {
            string[] args = new string[] { "-w", "-r", "..\\..\\..\\InputTest\\input_r4.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
        }
        [TestMethod]
        public void RTest_21()
        {
            string[] args = new string[] { "-c", "-r", "..\\..\\..\\InputTest\\input_r5.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec)); 
        }
        [TestMethod]
        public void RTest_22()
        {
            string[] args = new string[] { "-c", "-r","-h","n", "..\\..\\..\\InputTest\\input_r5.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));
        }
        [TestMethod]
        public void RTest_23()
        {
            string[] args = new string[] { "-c", "-r", "-t", "u", "..\\..\\..\\InputTest\\input_r5.txt" };
            WordListCommandLineSolver.CoreCommandLineSolver(args);
            string[] solution = FileReader();
            //Assert.IsTrue(CheckOutputLegal(solution, sc, ec));  
        }
        }
    }
