using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestWordChain
{
    /// <summary>
    /// Parse the commandline arg input
    ///     Result:
    ///         1. Exception flags  --  record whether exception has been raised
    ///             CommandLegal    --  结果合法（允许duplicate
    ///             CommandDupli    --  结果中有重复的cmd，无其他语法错误，可以选择使用去掉重复后的list作为算法模块的输入    ///             
    ///         2. ParsedCommands   --  store the parsed commands
    ///         3. DistinctCommand  --  store distinct version result (去掉重复，若无则 = parsed command
    ///     外部调用：
    ///         var parser = new CommandArgInputParser();
    ///         parser.parse();
    /// </summary>
    public class CommandArgInputParser
    {
        public readonly string FilePath;
        public bool CommandLegal
        {
            get;
            private set;
        }
        public bool CommandDuplicated
        {
            get;
            private set;
        }
        public string[] InitialCommands
        {
            get;
            private set;
        }

        public List<string> LegalKeyWordCommands
        {
            get;
            private set;
        }
        private List<Command> ParsedCommands;
        private List<Command> DistinctParsedCommands;

        #region ExceptionFlags
        public bool IllegalCommandKeyWord
        {
            get;
            private set;
        }
        public bool IllegalKeyWordCombination
        {
            get;
            private set;
        }
        public bool MissingMustContainedKeyWord
        {
            get;
            private set;
        }
        public bool InvalidStartChar
        {
            get;
            private set;
        }
        public bool StartCharMissing
        {
            get;
            private set;
        }
        public bool InvalidEndChar
        {
            get;
            private set;
        }
        public bool EndCharMissing
        {
            get;
            private set;
        }
        public bool UnExpectedChar
        {
            get;
            private set;
        }
        #endregion


        public CommandArgInputParser(string[] args)
        {
            FilePath = args[args.Length - 1];
            //slice input arg
            InitialCommands = new string[args.Length - 1];
            Array.ConstrainedCopy(args,0,InitialCommands,0,args.Length -1);

            LegalKeyWordCommands = new List<string>();
            ParsedCommands = new List<Command>();
            DistinctParsedCommands = new List<Command>();
            CommandLegal = true;
            CommandDuplicated = false;

            #region Init exception flags
            IllegalCommandKeyWord = false;
            IllegalKeyWordCombination = false;
            MissingMustContainedKeyWord = false;
            StartCharMissing = false;
            EndCharMissing = false;
            UnExpectedChar = false;
            #endregion
        }

        //将不合法的关键词去掉，结果存入LegalKeyWordCommands
        //例 "-c -w -x -t [ a -r" --> "-c -w -t a -r",若有不合法关键字，CommandLegal = false
        public void GetLegalKeyWords()
        {
            string ExtraExceptionMessage = "-->";
            foreach (string Word in InitialCommands)
            {
                if (Command.LegalCommands.Contains(Word))
                {
                    LegalKeyWordCommands.Add(Word);
                }
                else
                {
                    bool IllegalKeywordException = false;
                    if (Word.Length == 1)
                    {
                        char CharWord = Char.Parse(Word);
                        //rule out start or end char setting
                        if( !(( CharWord >= 'a' && CharWord <= 'z') || (CharWord >= 'A' && CharWord <= 'Z')))
                        {
                            IllegalKeywordException = true;
                            ExtraExceptionMessage += CharWord.ToString() + " ";
                        }
                        else
                        {
                            LegalKeyWordCommands.Add(CharWord.ToString());
                        }
                    }
                    else
                    {
                        IllegalKeywordException = true;
                        ExtraExceptionMessage += Word + " ";
                    }
                    if (IllegalKeywordException)
                    {
                        try
                        {
                            throw new IllegalCommandKeyWord();
                        }
                        catch(IllegalCommandKeyWord e)
                        {
                            Console.WriteLine(e.Message + ExtraExceptionMessage);
                            CommandLegal = false;
                            IllegalCommandKeyWord = true;
                        }
                    }
                }
            }
        }

        //在GetLegalKeyWords结果的基础上，检查
        //1.是否有-c -w之一
        //2.是否-c -w都有
        //设置相应的flag，出现其一就把commandLegal = false
        public void CheckCommandCombinationLegal()
        {
            //check combination           
            if(!(LegalKeyWordCommands.Contains("-c") || LegalKeyWordCommands.Contains("-w"))){
                try
                {
                    throw new MissingMustContainedKeyWord();
                }
                catch (MissingMustContainedKeyWord e){
                    Console.WriteLine(e.Message + "--> Should at least contain one of -c or -w command");
                    CommandLegal = false;
                    MissingMustContainedKeyWord = true;
                }
            }
            else
            {
                if (LegalKeyWordCommands.Contains("-c") && LegalKeyWordCommands.Contains("-w"))
                {
                    try
                    {
                        throw new IllegalKeyWordCombination();
                    }
                    catch(IllegalKeyWordCombination e)
                    {
                        Console.WriteLine(e.Message + "--> Should not contain both -c and -w command");
                        CommandLegal = false;
                        IllegalKeyWordCombination = true;
                    }
                }
            }
        }

        //在CheckCommandCombinationLegal结果基础上
        //检查重复关键字
        //生成ParsedCommands和DistinctCommand
        public void FormatInitialList()
        {
            
            void HandleMissingCharException(string CurrentWord)
            {
                if (CurrentWord == "-h")
                {
                    try
                    {
                        throw new StartCharMissing();
                    }
                    catch (StartCharMissing e)
                    {
                        Console.WriteLine(e.Message);
                        CommandLegal = false;
                        StartCharMissing = true;
                    }
                }
                else
                {
                    try
                    {
                        throw new EndCharMissing();
                    }
                    catch (EndCharMissing e)
                    {
                        Console.WriteLine(e.Message);
                        CommandLegal = false;
                        EndCharMissing = true;
                    }
                }
            }


            for(int i = 0; i < LegalKeyWordCommands.Count; i++)
            {
                string CurrentWord = LegalKeyWordCommands[i];
                // is command
                if (Command.LegalCommands.Contains(CurrentWord)){
                    //-t -h find next char
                    if ( CurrentWord == "-t" || CurrentWord == "-h")
                    {
                        //"-t" or "-h" at the end of the list(no char after the command)
                        if (i == LegalKeyWordCommands.Count - 1)
                        {
                            HandleMissingCharException(CurrentWord);
                        }
                        //try to get the char from next item
                        else
                        {
                            //next item after t h is also command
                            if (Command.LegalCommands.Contains(LegalKeyWordCommands[i + 1]))
                            {
                                HandleMissingCharException(CurrentWord);
                                continue;
                            }
                            //find legal char on next item(char has been filtered in GetLegalKeyWords func)
                            else
                            {
                                ParsedCommands.Add(new Command(CurrentWord, Char.Parse(LegalKeyWordCommands[i+1])));
                                i += 1;
                            }
                        }
                    }
                    //-w -c -r
                    else
                    {
                        ParsedCommands.Add(new Command(CurrentWord));
                    }                    
                }
                //single char exception
                else
                {
                    try
                    {
                        throw new UnExpectedChar();
                    }
                    catch (UnExpectedChar e)
                    {
                        Console.WriteLine(e.Message + "-->" + CurrentWord);
                        CommandLegal = false;
                        UnExpectedChar = true;
                    }
                }
            }
            //check duplicate commands and build distinct list
            foreach (Command ParsedCommand in ParsedCommands)
            {
                int Count = 0;
                if(ParsedCommand.CommandString != "-t" && ParsedCommand.CommandString != "-h")
                {
                    foreach (Command parsedCommand in ParsedCommands)
                    {
                        if (ParsedCommand.Equals(parsedCommand))
                        {
                            Count += 1;
                        }
                    }
                    if (!DistinctParsedCommands.Contains(ParsedCommand))
                    {
                        DistinctParsedCommands.Add(ParsedCommand);
                    }
                }
                else
                {
                    foreach (Command parsedCommand in ParsedCommands)
                    {
                        if (parsedCommand.CommandString == ParsedCommand.CommandString)
                        {
                            Count += 1;
                        }
                        if (Count == 1)
                        {
                            if (parsedCommand.Equals(ParsedCommand) && !DistinctParsedCommands.Contains(ParsedCommand))
                            {
                                DistinctParsedCommands.Add(ParsedCommand);
                            }
                        }
                    }
                }
                
                if(Count > 1)
                {
                    try
                    {
                        throw new DuplicateKeyCommand();
                    }
                    catch (DuplicateKeyCommand e)
                    {
                        Console.WriteLine(e.Message + "-->" + ParsedCommand.CommandString + " " + ParsedCommand.StartOrEndChar);
                        CommandDuplicated = true;
                    }
                }
                
            }
        }

        public void Parse()
        {
            GetLegalKeyWords();
            CheckCommandCombinationLegal();
            FormatInitialList();
        }

        public List<Command> GetParsedCommandList(bool distinct = false)
        {
            if (distinct)
            {
                return DistinctParsedCommands;
            }
            else
            {
                return ParsedCommands;
            }
        }

        public static string ConvertCommandListToString(List<Command> CommandList)
        {
            string WholeCommandString = "";
            foreach (Command command in CommandList)
            {
                WholeCommandString += command.CommandString + " ";
                if (command.StartOrEndChar != '\0')
                {
                    WholeCommandString += command.StartOrEndChar.ToString() + " ";
                }
            }
            return WholeCommandString;
        }

        public static List<string> ConvertCommandListToList(List<Command> CommandList)
        {
            List<string> CommandStringList = new List<string>();
            foreach (Command command in CommandList)
            {
                CommandStringList.Add(command.CommandString);
                if(command.StartOrEndChar != '\0')
                {
                    CommandStringList.Add(command.StartOrEndChar.ToString());
                }
            }
            return CommandStringList;
        }

    }

    //命令类，"-w" "-c" "-r" "-t EndChar" "-h StartChar"之一
    public class Command
    {
        public static readonly string[] LegalCommands = { "-w", "-c", "-r", "-t", "-h" };
        public readonly string CommandString;
        public readonly char StartOrEndChar;

        public Command(string _CommandString, char _StartOrEndChar = '\0')
        {
            CommandString = _CommandString;
            StartOrEndChar = _StartOrEndChar;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else
            {
                Command AnotherCommand = obj as Command;
                if(AnotherCommand == null)
                {
                    return false;
                }
                return (CommandString == AnotherCommand.CommandString && StartOrEndChar == AnotherCommand.StartOrEndChar);
            }
        }
        
    }
}

