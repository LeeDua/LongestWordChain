using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LongestWordChain;
using DeepEqual.Syntax;

namespace LongestWordChainCSharpTest
{
    /// <summary>
    /// CommandArgInputParserCSharpTest 的摘要说明
    /// </summary>
    [TestClass]
    public class CommandArgInputParserCSharpTest
    {

        public CommandArgInputParserCSharpTest()
        {
            
        }
        
        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestInitialize]
        public void TestInit()
        {
            
        }

        private string[] CorrectArg = { "-c", "-r", "-t", "a", "-h", "d", "SomeFilePath" };
        [TestMethod]
        public void CorrectArgTest()
        {
            CommandArgInputParser parser = new CommandArgInputParser(CorrectArg);
            parser.GetLegalKeyWords();
            Assert.IsTrue(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            parser.LegalKeyWordCommands.ShouldDeepEqual(new List<string>(parser.InitialCommands));

            parser.CheckCommandCombinationLegal();
            Assert.IsTrue(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);

            parser.FormatInitialList();
            Assert.IsFalse(parser.CommandDuplicated);
            Assert.IsTrue(parser.CommandLegal);
            parser.GetParsedCommandList(true).IsDeepEqual(parser.GetParsedCommandList(false));
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList()), "-c -r -t a -h d ");

            Assert.IsFalse(parser.IllegalCommandKeyWord);
            Assert.IsFalse(parser.IllegalKeyWordCombination);
            Assert.IsFalse(parser.MissingMustContainedKeyWord);
            Assert.IsFalse(parser.StartCharMissing);
            Assert.IsFalse(parser.EndCharMissing);
            Assert.IsFalse(parser.UnExpectedChar);

        }

        private string[] CorrectArg1 = { "-t", "x", "-h", "p", "-c", "SomeFilePath" };
        [TestMethod]
        public void CorrectArgTest1()
        {
            CommandArgInputParser parser = new CommandArgInputParser(CorrectArg1);
            parser.GetLegalKeyWords();
            Assert.IsTrue(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            parser.LegalKeyWordCommands.ShouldDeepEqual(new List<string>(parser.InitialCommands));

            parser.CheckCommandCombinationLegal();
            Assert.IsTrue(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);

            parser.FormatInitialList();
            Assert.IsFalse(parser.CommandDuplicated);
            Assert.IsTrue(parser.CommandLegal);
            parser.GetParsedCommandList(true).IsDeepEqual(parser.GetParsedCommandList(false));
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList()), "-t x -h p -c ");

            Assert.IsFalse(parser.IllegalCommandKeyWord);
            Assert.IsFalse(parser.IllegalKeyWordCombination);
            Assert.IsFalse(parser.MissingMustContainedKeyWord);
            Assert.IsFalse(parser.StartCharMissing);
            Assert.IsFalse(parser.EndCharMissing);
            Assert.IsFalse(parser.UnExpectedChar);
        }

        private string[] DuplicateKeyCommand_t = { "-c", "-r", "-t", "a", "-h", "d", "-t", "a", "SomeFilePath" };
        [TestMethod]
        public void DuplicateKeyCommand_t_Test()
        {
            CommandArgInputParser parser = new CommandArgInputParser(DuplicateKeyCommand_t);
            parser.GetLegalKeyWords();
            Assert.IsTrue(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            parser.LegalKeyWordCommands.ShouldDeepEqual(new List<string>(parser.InitialCommands));

            parser.CheckCommandCombinationLegal();
            Assert.IsTrue(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);

            parser.FormatInitialList();
            Assert.IsTrue(parser.CommandDuplicated);
            Assert.IsTrue(parser.CommandLegal);
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList()), "-c -r -t a -h d -t a ");
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList(true)), "-c -r -t a -h d ");

            Assert.IsFalse(parser.IllegalCommandKeyWord);
            Assert.IsFalse(parser.IllegalKeyWordCombination);
            Assert.IsFalse(parser.MissingMustContainedKeyWord);
            Assert.IsFalse(parser.StartCharMissing);
            Assert.IsFalse(parser.EndCharMissing);
            Assert.IsFalse(parser.UnExpectedChar);
        }

        private string[] DuplicateKeyCommand_c = { "-c", "-r", "-t", "a", "-h", "d", "-c", "SomeFilePath" };
        [TestMethod]
        public void DuplicateKeyCommand_c_Test()
        {
            CommandArgInputParser parser = new CommandArgInputParser(DuplicateKeyCommand_c);
            parser.GetLegalKeyWords();
            Assert.IsTrue(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            parser.LegalKeyWordCommands.ShouldDeepEqual(new List<string>(parser.InitialCommands));

            parser.CheckCommandCombinationLegal();
            Assert.IsTrue(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);

            parser.FormatInitialList();
            Assert.IsTrue(parser.CommandDuplicated);
            Assert.IsTrue(parser.CommandLegal);
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList()), "-c -r -t a -h d -c ");
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList(true)), "-c -r -t a -h d ");

            Assert.IsFalse(parser.IllegalCommandKeyWord);
            Assert.IsFalse(parser.IllegalKeyWordCombination);
            Assert.IsFalse(parser.MissingMustContainedKeyWord);
            Assert.IsFalse(parser.StartCharMissing);
            Assert.IsFalse(parser.EndCharMissing);
            Assert.IsFalse(parser.UnExpectedChar);
        }

        private string[] IllegalCommandKeyWord = { "-c", "-r", "-x", "-t", "a", "-h", "d", "-t", "-y", "a", "SomeFilePath" };
        [TestMethod]
        public void IllegalCommandKeyWord_Test()
        {
            CommandArgInputParser parser = new CommandArgInputParser(IllegalCommandKeyWord);
            parser.GetLegalKeyWords();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            parser.LegalKeyWordCommands.ShouldDeepEqual(new List<string>(new string[] { "-c", "-r", "-t", "a", "-h", "d", "-t", "a"}));
            Assert.IsTrue(parser.IllegalCommandKeyWord);

            parser.CheckCommandCombinationLegal();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);

            parser.FormatInitialList();
            Assert.IsTrue(parser.CommandDuplicated);
            Assert.IsFalse(parser.CommandLegal);
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList()), "-c -r -t a -h d -t a ");
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList(true)), "-c -r -t a -h d ");

            Assert.IsTrue(parser.IllegalCommandKeyWord);
            Assert.IsFalse(parser.IllegalKeyWordCombination);
            Assert.IsFalse(parser.MissingMustContainedKeyWord);
            Assert.IsFalse(parser.StartCharMissing);
            Assert.IsFalse(parser.EndCharMissing);
            Assert.IsFalse(parser.UnExpectedChar);
        }

        private string[] IllegalCommandKeyWord_1 = { "-c", "-r", "-t", "[", "a", "-h", "d", "SomeFilePath" };
        [TestMethod]
        public void IllegalCommandKeyWord_Test1()
        {
            CommandArgInputParser parser = new CommandArgInputParser(IllegalCommandKeyWord_1);
            parser.GetLegalKeyWords();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            parser.LegalKeyWordCommands.ShouldDeepEqual(new List<string>(new string[] { "-c", "-r", "-t", "a", "-h", "d" }));
            Assert.IsTrue(parser.IllegalCommandKeyWord);
            
            parser.CheckCommandCombinationLegal();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);

            parser.FormatInitialList();
            Assert.IsFalse(parser.CommandDuplicated);
            Assert.IsFalse(parser.CommandLegal);
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList()), "-c -r -t a -h d ");
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList(true)), "-c -r -t a -h d ");

            Assert.IsTrue(parser.IllegalCommandKeyWord);
            Assert.IsFalse(parser.IllegalKeyWordCombination);
            Assert.IsFalse(parser.MissingMustContainedKeyWord);
            Assert.IsFalse(parser.StartCharMissing);
            Assert.IsFalse(parser.EndCharMissing);
            Assert.IsFalse(parser.UnExpectedChar);
        }

        private string[] IllegalKeyWordCombination = { "-c", "-r", "-w", "-t", "a", "-h", "d", "-t", "a", "SomeFilePath" };
        [TestMethod]
        public void IllegalKeyWordCombination_Test()
        {
            CommandArgInputParser parser = new CommandArgInputParser(IllegalKeyWordCombination);
            parser.GetLegalKeyWords();
            Assert.IsTrue(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            parser.LegalKeyWordCommands.ShouldDeepEqual(new List<string>(new string[] { "-c", "-r","-w", "-t", "a", "-h", "d","-t","a" }));

            parser.CheckCommandCombinationLegal();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            Assert.IsTrue(parser.IllegalKeyWordCombination);

            parser.FormatInitialList();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsTrue(parser.CommandDuplicated);
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList()), "-c -r -w -t a -h d -t a ");
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList(true)), "-c -r -w -t a -h d ");

            Assert.IsFalse(parser.IllegalCommandKeyWord);
            Assert.IsTrue(parser.IllegalKeyWordCombination);
            Assert.IsFalse(parser.MissingMustContainedKeyWord);
            Assert.IsFalse(parser.StartCharMissing);
            Assert.IsFalse(parser.EndCharMissing);
            Assert.IsFalse(parser.UnExpectedChar);
        }
        private string[] MissingMustContainedKeyWord = { "-r", "-t", "a", "-h", "d", "SomeFilePath" };
        [TestMethod]
        public void MissingMustContainedKeyWord_Test()
        {
            CommandArgInputParser parser = new CommandArgInputParser(MissingMustContainedKeyWord);
            parser.GetLegalKeyWords();
            Assert.IsTrue(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            parser.LegalKeyWordCommands.ShouldDeepEqual(new List<string>(new string[] { "-r", "-t", "a", "-h", "d" }));

            parser.CheckCommandCombinationLegal();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            Assert.IsTrue(parser.MissingMustContainedKeyWord);
            
            parser.FormatInitialList();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList()), "-r -t a -h d ");
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList(true)), "-r -t a -h d ");

            Assert.IsFalse(parser.IllegalCommandKeyWord);
            Assert.IsFalse(parser.IllegalKeyWordCombination);
            Assert.IsTrue(parser.MissingMustContainedKeyWord);
            Assert.IsFalse(parser.StartCharMissing);
            Assert.IsFalse(parser.EndCharMissing);
            Assert.IsFalse(parser.UnExpectedChar);
        }

        private string[] InvalidEndChar = { "-c", "-r", "-t", "=", "-h", "d", "SomeFilePath" };
        [TestMethod]
        public void InvalidEndChar_Test()
        {
            CommandArgInputParser parser = new CommandArgInputParser(InvalidEndChar);
            parser.GetLegalKeyWords();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            parser.LegalKeyWordCommands.ShouldDeepEqual(new List<string>(new string[] { "-c", "-r", "-t", "-h", "d" }));
            Assert.IsTrue(parser.IllegalCommandKeyWord);

            parser.CheckCommandCombinationLegal();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);

            parser.FormatInitialList();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList()), "-c -r -h d ");
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList(true)), "-c -r -h d ");

            Assert.IsTrue(parser.IllegalCommandKeyWord);
            Assert.IsFalse(parser.IllegalKeyWordCombination);
            Assert.IsFalse(parser.MissingMustContainedKeyWord);
            Assert.IsFalse(parser.StartCharMissing);
            Assert.IsTrue(parser.EndCharMissing);
            Assert.IsFalse(parser.UnExpectedChar);
        }

        private string[] EndCharMissing = { "-c", "-r", "-t", "-h", "d", "SomeFilePath" };
        [TestMethod]
        public void EndCharMissing_Test()
        {
            CommandArgInputParser parser = new CommandArgInputParser(EndCharMissing);
            parser.GetLegalKeyWords();
            Assert.IsTrue(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            parser.LegalKeyWordCommands.ShouldDeepEqual(new List<string>(new string[] { "-c", "-r", "-t", "-h", "d" }));

            parser.CheckCommandCombinationLegal();
            Assert.IsTrue(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);

            parser.FormatInitialList();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList()), "-c -r -h d ");
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList(true)), "-c -r -h d ");

            Assert.IsFalse(parser.IllegalCommandKeyWord);
            Assert.IsFalse(parser.IllegalKeyWordCombination);
            Assert.IsFalse(parser.MissingMustContainedKeyWord);
            Assert.IsFalse(parser.StartCharMissing);
            Assert.IsTrue(parser.EndCharMissing);
            Assert.IsFalse(parser.UnExpectedChar);
        }

        private string[] InvalidStartChar = { "-c", "-r", "-h", "*", "SomeFilePath" };
        [TestMethod]
        public void InvalidStartChar_Test()
        {
            CommandArgInputParser parser = new CommandArgInputParser(InvalidStartChar);
            parser.GetLegalKeyWords();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            parser.LegalKeyWordCommands.ShouldDeepEqual(new List<string>(new string[] { "-c", "-r", "-h" }));

            parser.CheckCommandCombinationLegal();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);

            parser.FormatInitialList();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList()), "-c -r ");
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList(true)), "-c -r ");

            Assert.IsTrue(parser.IllegalCommandKeyWord);
            Assert.IsFalse(parser.IllegalKeyWordCombination);
            Assert.IsFalse(parser.MissingMustContainedKeyWord);
            Assert.IsTrue(parser.StartCharMissing);
            Assert.IsFalse(parser.EndCharMissing);
            Assert.IsFalse(parser.UnExpectedChar);

        }
        private string[] StartCharMissing = { "-c", "-r", "-h", "SomeFilePath" };
        [TestMethod]
        public void StartCharMissing_Test()
        {
            CommandArgInputParser parser = new CommandArgInputParser(StartCharMissing);
            parser.GetLegalKeyWords();
            Assert.IsTrue(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            parser.LegalKeyWordCommands.ShouldDeepEqual(new List<string>(new string[] { "-c", "-r", "-h" }));

            parser.CheckCommandCombinationLegal();
            Assert.IsTrue(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);

            parser.FormatInitialList();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList()), "-c -r ");
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList(true)), "-c -r ");

            Assert.IsFalse(parser.IllegalCommandKeyWord);
            Assert.IsFalse(parser.IllegalKeyWordCombination);
            Assert.IsFalse(parser.MissingMustContainedKeyWord);
            Assert.IsTrue(parser.StartCharMissing);
            Assert.IsFalse(parser.EndCharMissing);
            Assert.IsFalse(parser.UnExpectedChar);

        }

        private string[] UnExpectedChar = { "-c", "b", "-r", "a", "-h", "SomeFilePath" };
        [TestMethod]
        public void UnExpectedChar_Test()
        {
            CommandArgInputParser parser = new CommandArgInputParser(UnExpectedChar);
            parser.GetLegalKeyWords();
            Assert.IsTrue(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            parser.LegalKeyWordCommands.ShouldDeepEqual(new List<string>(new string[] { "-c","b", "-r", "a", "-h" }));

            parser.CheckCommandCombinationLegal();
            Assert.IsTrue(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);

            parser.FormatInitialList();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList()), "-c -r ");
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList(true)), "-c -r ");

            Assert.IsFalse(parser.IllegalCommandKeyWord);
            Assert.IsFalse(parser.IllegalKeyWordCombination);
            Assert.IsFalse(parser.MissingMustContainedKeyWord);
            Assert.IsTrue(parser.StartCharMissing);
            Assert.IsFalse(parser.EndCharMissing);
            Assert.IsTrue(parser.UnExpectedChar);
        }
        
        private string[] AllException1 = { "-x", "-c","-w", "b", "-r", "-h","-t", "SomeFilePath" };
        [TestMethod]
        public void AllExceptionTest_1()
        {
            CommandArgInputParser parser = new CommandArgInputParser(AllException1);
            parser.GetLegalKeyWords();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            parser.LegalKeyWordCommands.ShouldDeepEqual(new List<string>(new string[] { "-c", "-w","b","-r", "-h", "-t" }));

            parser.CheckCommandCombinationLegal();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);

            parser.FormatInitialList();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList()), "-c -w -r ");
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList(true)), "-c -w -r ");

            Assert.IsTrue(parser.IllegalCommandKeyWord);
            Assert.IsTrue(parser.IllegalKeyWordCombination);
            Assert.IsFalse(parser.MissingMustContainedKeyWord);
            Assert.IsTrue(parser.StartCharMissing);
            Assert.IsTrue(parser.EndCharMissing);
            Assert.IsTrue(parser.UnExpectedChar);
        }

        private string[] AllException2 = { "-x", "b", "-r", "-h", "-t", "SomeFilePath" };
        [TestMethod]
        public void AllExceptionTest_2()
        {
            CommandArgInputParser parser = new CommandArgInputParser(AllException2);
            parser.GetLegalKeyWords();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            parser.LegalKeyWordCommands.ShouldDeepEqual(new List<string>(new string[] { "b", "-r", "-h", "-t" }));

            parser.CheckCommandCombinationLegal();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);

            parser.FormatInitialList();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList()), "-r ");
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList(true)), "-r ");

            Assert.IsTrue(parser.IllegalCommandKeyWord);
            Assert.IsFalse(parser.IllegalKeyWordCombination);
            Assert.IsTrue(parser.MissingMustContainedKeyWord);
            Assert.IsTrue(parser.StartCharMissing);
            Assert.IsTrue(parser.EndCharMissing);
            Assert.IsTrue(parser.UnExpectedChar);
        }

        
        private string[] AllException3 = { "-x", "-c", "-w", "b", "-r", "-h", "-t", "-t", "a", "-t", "c", "-h", "x", "-h", "y", "SomeFilePath" };
        [TestMethod]
        public void AllExceptionTest_3()
        {
            CommandArgInputParser parser = new CommandArgInputParser(AllException3);
            parser.GetLegalKeyWords();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);
            parser.LegalKeyWordCommands.ShouldDeepEqual(new List<string>(new string[] { "-c", "-w", "b", "-r", "-h", "-t" ,"-t", "a", "-t", "c", "-h", "x", "-h", "y" }));

            parser.CheckCommandCombinationLegal();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);

            parser.FormatInitialList();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsTrue(parser.CommandDuplicated);
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList()), "-c -w -r -t a -t c -h x -h y ");
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList(true)), "-c -w -r -t a -h x ");

            Assert.IsTrue(parser.IllegalCommandKeyWord);
            Assert.IsTrue(parser.IllegalKeyWordCombination);
            Assert.IsFalse(parser.MissingMustContainedKeyWord);
            Assert.IsTrue(parser.StartCharMissing);
            Assert.IsTrue(parser.EndCharMissing);
            Assert.IsTrue(parser.UnExpectedChar);
        }
    }
}
