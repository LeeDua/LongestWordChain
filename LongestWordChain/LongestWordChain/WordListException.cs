using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// the class that defines different exception names
    /// inheritence structure:
    /// ApplicationException --> WordListException --> [CommandException, FileException]
    /// CommandException -> IllegalCommandKeyWord, IllegalKeyWordCombination, MissingMustContainedKeyWord
    ///     StartCharMissing, EndCharMissing, UnExpectedChar
    /// FileException --> FileOpenFailed, IllegalFileOnNonRCommand
    /// 
    /// 可以结合UnitTest理解各个错误的情景，UnitTest有点部分不只如函数名测了单个异常，看flag理解
    /// </summary>
    public abstract class WordListException : ApplicationException
    {
        
        protected WordListException(string message) : base(message)
        {

        }
    }

    public abstract class CommandException : WordListException
    {
        protected CommandException(string message) : base(message)
        {
            
        }    
    }

    public abstract class FileException : WordListException
    {
        protected FileException(string message) : base(message)
        {

        }
    }

    // 出现除了"-r -c -w -t -h"以外的关键字，如"-x"
    public class IllegalCommandKeyWord : CommandException
    {

        public IllegalCommandKeyWord(string message = "COMMAND_EXCEPTION:IllegalCommandKeyWord") : base(message)
        {

        }
    }

    //-c -w同时出现
    public class IllegalKeyWordCombination : CommandException
    {

        public IllegalKeyWordCombination(string message = "COMMAND_EXCEPTION:IllegalKeyWordCombination") : base(message)
        {

        }
    }

    //出现 -t a -t a 或 -w -w这种
    public class DuplicateKeyCommand : CommandException
    {

        public DuplicateKeyCommand(string message = "COMMAND_EXCEPTION:DuplicateKeyCommand") : base(message)
        {

        }
    }

    //-c -w至少要出现其一
    public class MissingMustContainedKeyWord : CommandException
    {
        public MissingMustContainedKeyWord(string message = "COMMAND_EXCEPTION:MissingMustContainedKeyWord") : base(message)
        {

        }
    }

    //-h 后面没有Char
    public class StartCharMissing : CommandException
    {
        public StartCharMissing(string message = "COMMAND_EXCEPTION:StartCharMissing") : base(message)
        {

        }
    }
    
    //-t 后面没有char
    public class EndCharMissing : CommandException
    {
        public EndCharMissing(string message = "COMMAND_EXCEPTION:EndCharMissing") : base(message)
        {

        }
    }

    //意外的Char,如 "-c a -t b"里的a
    public class UnExpectedChar : CommandException
    {
        public UnExpectedChar(string message = "COMMAND_EXCEPTION:UnExpectedChar") : base(message)
        {

        }
    }

    public class FileOpenFailed : FileException
    {
        public FileOpenFailed(string message = "FILE_PARSE_EXCEPTION:FileOpenFailed") : base(message)
        {

        }
    }

    //没有-r情况下文本有环
    public class IllegalFileOnNonRCommand : FileException
    {
        public IllegalFileOnNonRCommand(string message = "FILE_PARSE_EXCEPTION:IllegalFileOnNonRCommand") : base(message)
        {

        }
    }




}
