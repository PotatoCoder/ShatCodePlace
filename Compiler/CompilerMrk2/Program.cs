using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CompilerMrk2
{
    class Program
    {
        static void Main(string[] args)
        {
            Management manager = new Management();
            manager.Manage();

            Console.ReadLine();
        }
    }
}
