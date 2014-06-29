using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerMrk2
{

    /*
     * 
     * Structure for Classes and Constructors
     * 
     */

    public class ClassBlock
    {
        public List<BuildingBlock> buildingBlocks = new List<BuildingBlock>();
        public List<string> linesOfCode = new List<string>();
        // We might wanna make a variable class
        public List<object> variables = new List<object>();
        //Create and Store the Bracket locations
        public int startingLocation = 0;
        public int endingLocation = 0;

        //For later Use we need the class decleration
        public string classDecleration = "";
        public string Name = "";

        //Need to Jumpback to specfic building blocks
    }
}