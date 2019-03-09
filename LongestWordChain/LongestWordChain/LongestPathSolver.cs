using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestWordChain
{
    class LongestPathSolver
    {
        private readonly  List<Command> SolveSettings;
        private FileParser fileParser;

        public LongestPathSolver(List<Command> _SolveSettings, string _FilePath)
        {
            SolveSettings = _SolveSettings;
            fileParser = new FileParser(_FilePath);
        }

        private void TopoLogicalSort()
        {

        }

        private void NonAcyclicSover()
        {

        }
        private void AcylicSover()
        {

        }
        public int Solve()
        {
            //switch command option combination and call dll solver
            return 0;
        }
    }
}
