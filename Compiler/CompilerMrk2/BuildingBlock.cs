using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerMrk2
{
    /*
     * 
     * This block makes up the structure for Methods,function and loops
     * 
     */
    public class BuildingBlock
    {
        //Need to make this a Custom type eventually
        public string typeOfBlock = "";
        public string returnType = "";
        public List<string> linesOfCode = new List<string>();
        // Need to know when start and Stop locations are so we can grab lines of code
        public int startLocation = 0;
        public int endLocation = 0;
        //Decleration
        public string Decleration = "";
        public string Name = "";
        //Parameters
        //Type of Parameter,name of parameter,value of parameter
        public List<Tuple<string, string, string>> parameters = new List<Tuple<string, string, string>>();
        //Variables
        public VirtualBank vb = new VirtualBank();
    }
}
