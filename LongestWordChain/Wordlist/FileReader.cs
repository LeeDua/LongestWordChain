using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Core;

namespace Wordlist
{
    class FileReader
    {
        private string InputFilePath;
        public string[] FileData
        {
            get;
            private set;
        }
        public FileReader(string _InputFilePath)
        {
            InputFilePath = _InputFilePath;
        }
        
        public void ReadFile()
        {
            var ListData = new List<string>();
            FileStream fileStream;
            try
            {
                fileStream = new FileStream(InputFilePath, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.ASCII))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        ListData.Add(line);
                    }
                }
                FileData = ListData.ToArray();
            }
            catch(System.IO.IOException e)
            {
                try
                {
                    throw new FileOpenFailed();
                }
                catch(FileOpenFailed fe)
                {
                    Console.WriteLine(fe.Message);
                }
            }


           
        }

    }
}
