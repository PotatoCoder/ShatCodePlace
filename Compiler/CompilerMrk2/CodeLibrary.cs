using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace CompilerMrk2
{
    /*
     * Stores all of our predefined methods and will handle all math and condtional operations
     */
    public class CodeLibrary
    {

        public void Librarian(BuildingBlock b, Coin coin)
        {
            LibraryBooks lb = new LibraryBooks();

            //We need to run and call methods based on type of coin

            switch (coin.typeOfCoin)
            {
                case "variable":
                    break;
                case "coreMethod":
                    Type type = typeof(LibraryBooks);
                    MethodInfo info = type.GetMethod(coin.name);
                    GetParameterValue(b.vb.Repository["variables"], coin);
                    if (coin.parameters.Count() != 0)
                    {
                        info.Invoke(lb, new object[] { coin.parameters });
                    }
                    else
                    {
                        BlankBook bb = new BlankBook();
                        Type blank = typeof(BlankBook);
                        MethodInfo blankInfo = blank.GetMethod(coin.name);
                        blankInfo.Invoke(bb, null);
                    }
                    break;
                case "userMethod":
                    Vault methodCalls = coin.building_block.vb.Repository["Calls"];
                    //Before we call, need to send parameter values to new vault in calling method
                    if (coin.building_block.parameters.Count() > 0)
                    {
                        PassParameters(b, coin.building_block);
                    }
                    foreach (Coin userCoin in methodCalls.coins)
                    {
                        Librarian(coin.building_block, userCoin);
                    }
                    break;
                case "userInput":
                    CreateUserVariable(coin.building_block, coin);
                    break;
                case "newInstance":
                    //This will be for constructors if we put them in
                    break;
                default:
                    break;

            }

        }

        public void PassParameters(BuildingBlock oldBuilding, BuildingBlock newBuilding)
        {
            bool passFlag = false;

            for (int i = 0; i < oldBuilding.vb.Repository["variables"].coins.Count(); i++)
            {
                for (int j = 0; j < newBuilding.vb.Repository["variables"].coins.Count(); j++)
                {
                    if (oldBuilding.vb.Repository["variables"].coins[i].name.Equals(newBuilding.vb.Repository["variables"].coins[j].name))
                    {
                        newBuilding.vb.Repository["variables"].coins[j].value = oldBuilding.vb.Repository["variables"].coins[i].value;
                        passFlag = true;
                    }
                }
            }
        }

        public void GetParameterValue(Vault v, Coin coin)
        {
            for (int i = 0; i < v.coins.Count(); i++)
            {
                for (int j = 0; j < coin.parameters.Count(); j++)
                {
                    if (v.coins[i].name.Equals(coin.parameters[j]))
                    {
                        coin.parameters[j] = v.coins[i].value;
                    }
                }
            }
        }

        public void CreateUserVariable(BuildingBlock b, Coin coin)
        {
            for (int i = 0; i < b.vb.Repository["variables"].coins.Count(); i++)
            {
                if(b.vb.Repository["variables"].coins[i].name.Equals(coin.name))
                {
                    b.vb.Repository["variables"].coins[i].value = Console.ReadLine();
                }
            }
        }

        //if (!b.vb.Repository["variables"].coins[i].name.Equals(coin.name))
        //        {
        //            coin.value = Console.ReadLine();
        //            b.vb.Repository["variables"].coins.Add(coin);
        //        }
        //        else if (b.vb.Repository["variables"].coins.Contains(coin))
        //        {
        //            Coin valueCoin = b.vb.Repository["variables"].coins.Find(n => n.name.Equals(coin.name));
        //            int index = b.vb.Repository["variables"].coins.IndexOf(valueCoin);
        //            b.vb.Repository["variables"].coins[index].value = Console.ReadLine();
        //        }

        public class LibraryBooks
        {
            public void console_writeline(List<object> input)
            {
                Console.WriteLine(input[0]);
            }
        }
        // This class will hold method calls that are in LibraryBook when they have no parameters
        public class BlankBook
        {
            public void console_writeline()
            {
                Console.WriteLine();
            }

            public void console_readline()
            {
                Console.ReadLine();
            }

        }



    }
}
