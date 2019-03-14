using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// The actual algorism solver
    /// 和c++算法对接，通dll调用传入参数设置，返回处理结果（状态
    /// </summary>
    class LongestPathSolver
    {
        private readonly  List<Command> SolveSettings;
        public  FileParser fileParser;

        public LongestPathSolver(List<Command> _SolveSettings, string _FilePath)
        {
            SolveSettings = _SolveSettings;
            fileParser = new FileParser(_FilePath);
        }

       
        [DllImport(@"CoreBase.dll",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi,
            EntryPoint = "get_wordchain",
            ExactSpelling = false,
            SetLastError = true)]
        public static extern int get_wordchain(string[] args, int len);

 
        public int Solve()
        {
            List<string> FinalCommandList = CommandArgInputParser.ConvertCommandListToList(SolveSettings);
            FinalCommandList.Add(fileParser.FilePath);
            string[] args = FinalCommandList.ToArray();
            int ResultCode = get_wordchain(args, args.Length);
            return ResultCode;
        }

      
    }
}
