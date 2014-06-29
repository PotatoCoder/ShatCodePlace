using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class StackTheBlocks
    {
        /* 
         * 
         * The Purpose of the this class is to arrange the blocks in the correct order based on which blocks need what
         * Then we can execute them in order, this will also rearrnage the code blocks based on what code needs
         * Also we will keep track of class and function calls between classes in here
         */
        public StackTheBlocks()
        {
            Parser p = new Parser();
            Stacking(p.Parse(p.GrabDataFile())); ;

        }

        public void Stacking(List<ClassBlock> blocksToStack)
        {
            foreach(ClassBlock classB in blocksToStack)
            {
                foreach(CodeBlocks cBlock in classB.blocks)
                {
                    if(!cBlock.methodName.Equals(""))
                    {
                        MethodParser mp = new MethodParser();
                        mp.MethodParse(cBlock);
                    }
                }
            }
            Console.ReadLine();
        }

    }
}
