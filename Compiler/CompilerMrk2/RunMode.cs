using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerMrk2
{
    /*
     * RUN THINGS HERE
     */
    public class RunMode
    {
        public void Run(CoreBlock core)
        {
           CodeLibrary cl = new CodeLibrary();

           foreach(ClassBlock c in core.classBlocks)
           {
               foreach (BuildingBlock b in c.buildingBlocks)
               {
                   if(b.Name.Equals("Main"))
                   {
                       Vault methodCalls = b.vb.Repository["Calls"];
                       foreach (Coin coin in methodCalls.coins)
                       {
                           cl.Librarian(b, coin);
                       }
                   }
               }
           }
        }
    }
}
