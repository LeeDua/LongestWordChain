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

            parser.CheckCommandCombinationLegal();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);

            parser.FormatInitialList();
            Assert.IsTrue(parser.CommandDuplicated);
            Assert.IsFalse(parser.CommandLegal);
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList()), "-c -r -t a -h d -t a ");
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList(true)), "-c -r -t a -h d ");

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

            parser.CheckCommandCombinationLegal();
            Assert.IsFalse(parser.CommandLegal);
            Assert.IsFalse(parser.CommandDuplicated);

            parser.FormatInitialList();
            Assert.IsFalse(parser.CommandDuplicated);
            Assert.IsFalse(parser.CommandLegal);
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList()), "-c -r -t a -h d ");
            Assert.AreEqual(CommandArgInputParser.ConvertCommandListToString(parser.GetParsedCommandList(true)), "-c -r -t a -h d ");

        }

        private string[] IllegalKeyWordCombination = { "-c", "-r", "-w", "-t", "a", "-h", "d", "-t", "a", "SomeFilePath" };
        private string[] MissingMustContainedKeyWord = { "-r", "-t", "a", "-h", "d", "SomeFilePath" };
        private string[] InvalidStartChar = { "-c", "-r", "-t", "=", "-h", "d", "SomeFilePath" };
        private string[] StartCharMissing = { "-c", "-r", "-t", "-h", "d", "SomeFilePath" };
        private string[] InvalidEndChar = { "-c", "-r", "-h", "*", "SomeFilePath" };
        private string[] EndCharMissing = { "-c", "-r", "-h", "SomeFilePath" };
        private string[] UnExpectedChar = { "-c", "b", "-r", "a", "-h", "SomeFilePath" };

    }
}
