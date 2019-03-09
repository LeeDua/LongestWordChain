using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestWordChain
{
    
    abstract class WordListException : ApplicationException
    {
        
        protected WordListException(string message) : base(message)
        {

        }
    }
    
    abstract class CommandException : WordListException
    {
        protected CommandException(string message) : base(message)
        {
            
        }    
    }

    abstract class FileException : WordListException
    {
        protected FileException(string message) : base(message)
        {

        }
    }

    class IllegalCommandKeyWord : CommandException
    {

        public IllegalCommandKeyWord(string message = "COMMAND_EXCEPTION:IllegalCommandKeyWord") : base(message)
        {

        }
    }

    class IllegalKeyWordCombination : CommandException
    {

        public IllegalKeyWordCombination(string message = "COMMAND_EXCEPTION:IllegalKeyWordCombination") : base(message)
        {

        }
    }

    class DuplicateKeyCommand : CommandException
    {

        public DuplicateKeyCommand(string message = "COMMAND_EXCEPTION:DuplicateKeyCommand") : base(message)
        {

        }
    }

    class MissingMustContainedKeyWord : CommandException
    {
        public MissingMustContainedKeyWord(string message = "COMMAND_EXCEPTION:MissingMustContainedKeyWord") : base(message)
        {

        }
    }

    class InvalidStartChar : CommandException
    {
        public InvalidStartChar(string message = "COMMAND_EXCEPTION:InvalidStartChar") : base(message)
        {

        }
    }

    class StartCharMissing : CommandException
    {
        public StartCharMissing(string message = "COMMAND_EXCEPTION:StartCharMissing") : base(message)
        {

        }
    }

    class InvalidEndChar : CommandException
    {
        public InvalidEndChar(string message = "COMMAND_EXCEPTION:InvalidEndChar") : base(message)
        {

        }
    }

    class EndCharMissing : CommandException
    {
        public EndCharMissing(string message = "COMMAND_EXCEPTION:EndCharMissing") : base(message)
        {

        }
    }

    class UnExpectedChar : CommandException
    {
        public UnExpectedChar(string message = "COMMAND_EXCEPTION:UnExpectedChar") : base(message)
        {

        }
    }

    class FileOpenFailed : FileException
    {
        public FileOpenFailed(string message = "FILE_PARSE_EXCEPTION:FileOpenFailed") : base(message)
        {

        }
    }

    class IllegalFileOnNonRCommand : FileException
    {
        public IllegalFileOnNonRCommand(string message = "FILE_PARSE_EXCEPTION:IllegalFileOnNonRCommand") : base(message)
        {

        }
    }




}
