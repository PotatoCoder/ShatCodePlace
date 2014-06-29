using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class CodeBlocks
    {
        /* 
         * Purpose of this class is to keep track and maintain the classes and methods depending on bracket postion
         * Classes will be assigned Code blocks
         */
        public string scopeLevel { get; set; }
        public string methodName { get; set; }
        public Type returnType { get; set; }
        public List<string> parameters { get; set; }
        public int startPos { get; set; }
        public int endPos { get; set; }
        public List<string> codeLines = new List<string>();
    }
}
