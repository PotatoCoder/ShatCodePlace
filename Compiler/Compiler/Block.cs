using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public interface Block
    {
        string scopeLevel { get; set; }
        string Name { get; set; }
        Type returnType { get; set; }
        List<string> parameters { get; set; }
        int startPos { get; set; }
        int endPos { get; set; }
        List<string> createCodeLines();
        List<object> createCodeBlocks();
    }
}
