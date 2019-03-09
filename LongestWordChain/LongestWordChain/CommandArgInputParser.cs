using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestWordChain
{
    class CommandArgInputParser
    {
        public readonly string FilePath;
        public bool CommandLegal = true;
        public bool CommandDuplicated = false;
        private string[] InitialCommands;
        private List<string> LegalKeyWordCommands = new List<string>();
        private List<Command> ParsedCommands = new List<Command>();
        private List<Command> DistinctParsedCommands = new List<Command>();

        public bool IsCommandLegal()
        {
            return CommandLegal;
        }

        public bool IsCommandDuplicate()
        {
            return CommandDuplicated;
        }

        public CommandArgInputParser(string[] args)
        {
            FilePath = args[args.Length - 1];
            Array.Copy(args,InitialCommands,args.Length -1);
        }

        private void GetLegalKeyWords()
        {
            string ExtraExceptionMessage = "-->";
            foreach (string Word in InitialCommands)
            {
                if (Command.LegalCommands.Contains<string>(Word))
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
                        }
                    }
                }
            }
        }
        

        private void CheckCommandCombinationLegal()
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
                    }
                }
            }
        }

        private void FormatInitialList()
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
                                i += 1;
                                continue;
                            }
                            //find legal char on next item(char has been filtered in GetLegalKeyWords func)
                            else
                            {
                                ParsedCommands.Add(new Command(CurrentWord, Char.Parse(LegalKeyWordCommands[i+1])));
                            }
                        }
                    }
                    //-w -c -r
                    else
                    {
                        ParsedCommands.Add(new Command(CurrentWord, '\0'));
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
                    }
                }
            }
            //check duplicate commands and build distinct list
            foreach (Command ParsedCommand in ParsedCommands)
            {
                int Count = 0;
                foreach (Command parsedCommand in ParsedCommands)
                {
                    if (ParsedCommand == parsedCommand)
                    {
                        Count += 1;
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
                if (!DistinctParsedCommands.Contains(ParsedCommand))
                {
                    DistinctParsedCommands.Add(ParsedCommand);
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
                WholeCommandString += command.CommandString;
                WholeCommandString += command.StartOrEndChar.ToString();
            }
            return WholeCommandString;
        }

    }

    class Command
    {
        public static readonly string[] LegalCommands = { "-w", "-c", "-r", "-t", "-h" };
        public readonly string CommandString;
        public readonly char StartOrEndChar;

        public Command(string _CommandString, char _StartOrEndChar)
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

