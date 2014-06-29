using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class BlockStorage
    {
        public class CodeBlock : Block
        {

            public string scopeLevel
            {
                get;
                set;
            }

            public string Name
            {
                get;
                set;
            }

            public Type returnType
            {
                get;
                set;
            }

            public List<string> parameters
            {
                get;
                set;
            }

            public int startPos
            {
                get;
                set;
            }

            public int endPos
            {
                get;
                set;
            }

            public List<string> createCodeLines()
            {
                return new List<string>();
            }

            public List<object> createCodeBlocks()
            {
                return null;
            }
        }
        public class ClassBlock : Block
        {

            public string scopeLevel
            {
                get;
                set;
            }

            public string Name
            {
                get;
                set;
            }

            public Type returnType
            {
                get;
                set;
            }

            public List<string> parameters
            {
                get;
                set;
            }

            public int startPos
            {
                get;
                set;
            }

            public int endPos
            {
                get;
                set;
            }

            public List<string> createCodeLines()
            {
                return null;
            }

            public List<object> createCodeBlocks()
            {
                return new List<object>();
            }
        }
    }
}
