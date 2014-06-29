using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class ClassBlock
    {
        public string className { get; set; }
        public string ScopeLevel { get; set; }
        public List<CodeBlocks> blocks { get; set; }
    }
}
