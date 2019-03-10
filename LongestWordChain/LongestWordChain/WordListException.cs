using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestWordChain
{

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

    public class IllegalCommandKeyWord : CommandException
    {

        public IllegalCommandKeyWord(string message = "COMMAND_EXCEPTION:IllegalCommandKeyWord") : base(message)
        {

        }
    }

    public class IllegalKeyWordCombination : CommandException
    {

        public IllegalKeyWordCombination(string message = "COMMAND_EXCEPTION:IllegalKeyWordCombination") : base(message)
        {

        }
    }

    public class DuplicateKeyCommand : CommandException
    {

        public DuplicateKeyCommand(string message = "COMMAND_EXCEPTION:DuplicateKeyCommand") : base(message)
        {

        }
    }

    public class MissingMustContainedKeyWord : CommandException
    {
        public MissingMustContainedKeyWord(string message = "COMMAND_EXCEPTION:MissingMustContainedKeyWord") : base(message)
        {

        }
    }

    public class StartCharMissing : CommandException
    {
        public StartCharMissing(string message = "COMMAND_EXCEPTION:StartCharMissing") : base(message)
        {

        }
    }
    
    public class EndCharMissing : CommandException
    {
        public EndCharMissing(string message = "COMMAND_EXCEPTION:EndCharMissing") : base(message)
        {

        }
    }

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

    public class IllegalFileOnNonRCommand : FileException
    {
        public IllegalFileOnNonRCommand(string message = "FILE_PARSE_EXCEPTION:IllegalFileOnNonRCommand") : base(message)
        {

        }
    }




}
