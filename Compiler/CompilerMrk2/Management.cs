using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace CompilerMrk2
{

    /*
     * 
     * This Class has one purpose and that is to manage the entire app, we should not
     * put alot of construction components in here
     * 
     */
    public class Management
    {
        public void Manage()
        {
            string[] lines = File.ReadAllLines(@"C:\Users\Wes\Source\Workspaces\VirtualClassroom\Compiler\CompilerMrk2\SampleData.txt");
            BlockConstruction bc = new BlockConstruction();
            CoreBlock runCore = new CoreBlock();
            CoinCollector cc = new CoinCollector();
            RunMode rm = new RunMode();
            runCore = bc.Build(lines);
            cc.Collect(runCore);
            rm.Run(runCore);
        }
    }
}
