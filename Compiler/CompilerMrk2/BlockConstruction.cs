using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CompilerMrk2
{
    public class BlockConstruction
    {
        public CoreBlock Build(string[] lines)
        {
            //Build all Unspecfied blocks
            Dictionary<int, Tuple<int, int>> RandomBlocks = new Dictionary<int, Tuple<int, int>>();
            BuildBlocks(lines, RandomBlocks);
            // Create Core Block at begining so we can add classes to it
            CoreBlock core = new CoreBlock();


            // This will keep track of class blocks in list
            int classBlockIndex = 0;


            //We now need to search for a code line that is the creation of a class
            for (int i = 0; i < lines.Count(); i++)
            {

                if (lines[i].Contains("class"))
                {
                    core.classBlocks.Add(new ClassBlock());
                    core.classBlocks[classBlockIndex].classDecleration = lines[i].Trim();
                    string[] s1 = core.classBlocks[classBlockIndex].classDecleration.Split(' ');
                    string[] s2 = s1[2].Split('(');
                    core.classBlocks[classBlockIndex].Name = s2[0];
                    core.classBlocks[classBlockIndex].startingLocation = i + 1;
                    classBlockIndex++;
                }
            }

            //Determine which block in RandomBlocks are class Blocks
            // Then Assign the Ending Location so we can collect the lines of code between them


            foreach (ClassBlock b in core.classBlocks)
            {
                b.endingLocation = EndBlock(b.startingLocation, RandomBlocks);
            }


            //Now Assign lines of code to class block
            foreach (ClassBlock b in core.classBlocks)
            {
                b.linesOfCode = GetCode(b.startingLocation, b.endingLocation, lines);
            }


            //Now that we have all the classes and the lines between them, we now need to determine all the variables,loops, methods etc
            //Inside the classes

            foreach (ClassBlock class_block in core.classBlocks)
            {
                MoarBlocks(class_block);
            }

            /*
             * 
             * We are done making Blocks, I think :D
             * 
             * Now we just need to run everything inside the buildingblocks
             * 
             */
            return core;
        }

        public void MoarBlocks(ClassBlock class_block)
        {
            // We need to check to see what each line is

            Dictionary<int, Tuple<int, int>> MoarRandomBlocks = new Dictionary<int, Tuple<int, int>>();
            BuildBlocks(class_block.linesOfCode.ToArray(), MoarRandomBlocks);

            for (int i = 0; i < class_block.linesOfCode.Count(); i++)
            {
                string[] switch_Case = class_block.linesOfCode[i].Split(' ');
                switch (switch_Case[0])
                {
                    case "static":
                        BuildingBlock staticBlock = new BuildingBlock();
                        staticBlock.typeOfBlock = "method";
                        staticBlock.Decleration = class_block.linesOfCode[i];
                        BuildParameters(staticBlock, staticBlock.Decleration);
                        string[] s1 = staticBlock.Decleration.Split(' ');
                        string[] s2 = s1[2].Split('(');
                        staticBlock.Name = s2[0];
                        staticBlock.startLocation = i + 1;
                        staticBlock.endLocation = EndBlock(staticBlock.startLocation, MoarRandomBlocks);
                        staticBlock.linesOfCode = GetCode(staticBlock.startLocation, staticBlock.endLocation, class_block.linesOfCode.ToArray());
                        class_block.buildingBlocks.Add(staticBlock);
                        break;
                    case "void":
                        //BuildingBlock voidBlock = new BuildingBlock();
                        //voidBlock.typeOfBlock = "method";
                        //voidBlock.startLocation = i + 1;
                        //voidBlock.endLocation = EndBlock(voidBlock.startLocation);
                        //voidBlock.linesOfCode = GetCode(voidBlock.startLocation, voidBlock.endLocation, class_block.linesOfCode.ToArray());
                        //class_block.buildingBlocks.Add(voidBlock);
                        break;
                    case "int":
                        break;
                    case "string":
                        break;
                    default:
                        break;
                }

            }

        }

        //Before Seperating the blocks out, we need to create all the unspecfied blocks
        //Then we will match the blocks with the starting brackets index

        public void BuildBlocks(string[] lines, Dictionary<int, Tuple<int, int>> RandomBlocks)
        {
            // Tuple goes startBracket,EndBracket posistions

            List<int> sBrackets = new List<int>();

            for (int i = 0; i < lines.Count(); i++)
            {
                if (lines[i].Contains("{"))
                {
                    // This Adds in the Line Number where the { are
                    sBrackets.Add(i);
                }
                if (lines[i].Contains("}"))
                {
                    Worker(sBrackets, i, RandomBlocks);
                }
            }
        }

        public List<int> Worker(List<int> indexs, int loc, Dictionary<int, Tuple<int, int>> RandomBlocks)
        {
            RandomBlocks.Add(RandomBlocks.Count(), new Tuple<int, int>(indexs.Last(), loc));
            indexs.Remove(indexs.Last());
            return indexs;
        }

        public List<string> GetCode(int start, int end, string[] lines)
        {
            List<string> codeLines = new List<string>();
            for (int i = start; i <= end; i++)
            {
                codeLines.Add(lines[i].Trim());
            }
            return codeLines;
        }

        public int EndBlock(int start, Dictionary<int, Tuple<int, int>> RandomBlocks)
        {
            foreach (Tuple<int, int> t in RandomBlocks.Values)
            {
                if (start == t.Item1)
                {
                    return t.Item2;
                }
            }
            return 0;
        }
        // We need to create and store parameters in the blocks for later use
        public void BuildParameters(BuildingBlock b, string s)
        {
            //EX: void test(string s, int w)
            //Isolate Parameters
            string[] p1 = s.Split('(', ')');
            //Split them up
            string[] p2 = p1[1].Split(',');
            for(int i = 0; i < p2.Count(); i++)
            {
                string[] p3 = p2[i].Trim().Split(' ');
                if (!p3[0].Equals(""))
                {
                    b.parameters.Add(new Tuple<string, string, string>(p3[0], p3[1], ""));
                }
            }
        }
    }
}
