using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
namespace Compiler
{
    public class Parser
    {
        /*
         * Brackets are something we need to keep track of, if a bracket is found we need to stop and search for the ending bracket 
         * so we know where methods,classes etc are.
         * 
         */

        /*
         * 
         * Heavy duty, find all the classes
         * When a class is found we create code blocks for that class
         * 
         */
        public List<ClassBlock> Parse(List<string> data)
        {
            List<ClassBlock> classes = new List<ClassBlock>();
            List<string> classComponents = new List<string>();
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Contains("class"))
                {
                    string[] preclassComponents = data[i].Split(' ');
                    foreach (string s in preclassComponents)
                    {
                        if (s != "")
                        {
                            classComponents.Add(s);
                        }
                    }

                    ClassBlock cBlock = new ClassBlock();
                    cBlock.ScopeLevel = classComponents[0];
                    cBlock.className = classComponents[2];
                    cBlock.blocks = CreateCodeBlocks(data);
                    classes.Add(cBlock);
                }
            }
            return classes;
        }

        // Will Scan the blocks for variables, need to mark variables as public,private
        public void Scan(List<string> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                //We have each line now we need to spilt each line into compoents and create code blocks
                string[] components = data[i].Split(' ');
            }

        }

        public List<string> GrabDataFile()
        {
            List<string> fileData = new List<string>();
            foreach (var s in File.ReadAllLines(@"C:\Users\Wes\Source\Workspaces\VirtualClassroom\Compiler\Compiler\ExampleData.txt"))
            {
                fileData.Add(s);
            }
            return fileData;

        }

        // This needs to return a collection of blocks
        public List<CodeBlocks> CreateCodeBlocks(List<string> codeData)
        {
            CodeBlocks block = new CodeBlocks();
            List<int> startingBrackets = new List<int>();
            List<int> endingBrackets = new List<int>();
            List<CodeBlocks> blocks = new List<CodeBlocks>();

            for (int i = 0; i < codeData.Count; i++)
            {
                if (codeData[i].Contains("{"))
                {
                    startingBrackets.Add(codeData.IndexOf(codeData[i]));
                }
                if (codeData[i].Contains("}"))
                {
                    endingBrackets.Add(codeData.IndexOf(codeData[i]));
                }
            }


            foreach (int i in startingBrackets)
            {
                blocks.Add(BlockHeaders(i, codeData));
            }

            //Need to Assign each block a starting and endpoint so we can grab all the code inbetween it
            int j = endingBrackets.Count - 1;
            for (int i = 0; i < startingBrackets.Count; i++)
            {
                if (blocks[i].methodName != "")
                {
                    int index = startingBrackets[i];
                    for (int y = index; y < codeData.Count; y++)
                    {
                        int newEndPoint = codeData[y].Trim().IndexOf("}");
                        if (newEndPoint >= 0)
                        {
                            endingBrackets[j] = y;
                            break;
                        }
                    }
                }


                blocks[i].startPos = startingBrackets[i];
                blocks[i].endPos = endingBrackets[j];
                j--;


            }
            GrabCode(blocks, codeData);
            return blocks;
        }

        /*
         *  So we need to make logic for the code block headers
         *  1. Loops
         *  2. If Statements
         *  3. etc
         *  Need to go through each of the header logic methods each time you are about to create a new code block
         * 
         */
        public CodeBlocks BlockHeaders(int start, List<string> codeData)
        {
            CodeBlocks methodBlock = new CodeBlocks();
            List<string> cc = new List<string>();
            string commandLine = codeData[start - 1];


            string[] components = commandLine.Split(' ');

            foreach (string s in components)
            {
                if (s != "")
                {
                    cc.Add(s);
                }
            }
            switch (cc[0].Trim())
            {
                case "if":
                    break;
                case "while":
                    break;
                case "for":
                    break;
                //This is if its a method, for now testing need to not hard code these
                case "static":
                    //Get Variables
                    int startingPara = commandLine.IndexOf('(');
                    int endingPara = commandLine.IndexOf(')');
                    if (endingPara > 0 && startingPara > 0)
                    {
                        string varLine = commandLine.Substring(startingPara, endingPara - startingPara);
                        methodBlock.parameters = varLine.Split(',').ToList();
                    }
                    methodBlock.scopeLevel = cc[0].Trim();
                    methodBlock.returnType = Type.GetType(cc[1]);
                    methodBlock.methodName = cc[2];

                    // We need to figure out how to grab parameters out of ( )
                    break;
                default:
                    methodBlock.methodName = "";
                    break;
            }
            return methodBlock;
        }

        public List<CodeBlocks> GrabCode(List<CodeBlocks> blocks, List<string> codeData)
        {
            foreach (CodeBlocks cBlock in blocks)
            {
                for (int i = cBlock.startPos; i <= cBlock.endPos; i++)
                {
                    if (!codeData[i].Equals(""))
                    {
                        cBlock.codeLines.Add(codeData[i]);
                    }
                }
            }
            return blocks;

        }
    }
}
