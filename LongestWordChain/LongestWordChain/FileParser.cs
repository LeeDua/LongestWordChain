using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestWordChain
{
    /// <summary>
    /// a file parse interface, when the two boolean var is set to false, raise exception
    /// </summary>
    class FileParser
    {
        public readonly string FilePath;

        public bool IllegalFileOnNonRCommand
        {
            get
            {
                return IllegalFileOnNonRCommand;
            }
            set
            {
                if(value == false)
                {
                    IllegalFileOnNonRCommand = false;
                    Console.WriteLine("File legal -- not Acyclic on non -r condition");
                }
                else
                {
                    try
                    {
                        throw new IllegalFileOnNonRCommand();
                    }
                    catch(IllegalKeyWordCombination e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                
            }
        }

        public bool FileOpenFailed
        {
            get
            {
                return FileOpenFailed;
            }
            set
            {
                if(value == true)
                {
                    try
                    {
                        throw new FileOpenFailed();
                    }
                    catch(FileOpenFailed e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        public FileParser(string _FilePath)
        {
            FilePath = _FilePath;
        }
    }
}
