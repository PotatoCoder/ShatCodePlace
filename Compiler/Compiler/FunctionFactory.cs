using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class FunctionFactory
    {
        /*
         *  Factory will store all of the functions we need to use
         *  i want to try and implement Factory design pattern here i think it will work
         */ 

        //Trying to stay away from using Console here incase we run into problems later
        Dictionary<string, Delegate> functions = new Dictionary<string,Delegate>();

        public void CreateFunctions()
        {
            functions.Add("console.writeline",new Action<string>(LogWriteLine));
        }
        
        public void LogWriteLine(string s)
        {

        }

        public void LogReadLine()
        {

        }

    }
}
