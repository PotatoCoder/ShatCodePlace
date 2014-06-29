using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerMrk2
{

    /*
     * Each Class/Method has a Bank
     * 
     */

    public class VirtualBank
    {
        //Dictionary has Name of vault and vault associated with name
        public Dictionary<string, Vault> Repository = new Dictionary<string, Vault>();

        public void Deposit(string name,Coin vaultCoin)
        {
            for (int i = 0; i <= Repository.Count(); i++)
            {
                if(Repository.Keys.Contains(name))
                {
                    if (!Repository[name].coins.Contains(vaultCoin))
                    {
                        Repository[name].coins.Add(vaultCoin);
                    }
                }
                if(!Repository.Keys.Contains(name))
                {
                    Repository.Add(name, new Vault());
                    i--;
                }
            }
                
        }

        public object Withdraw(Coin vaultCoin)
        {
            foreach(Vault v in Repository.Values)
            {
                for(int i = 0; i < v.coins.Count(); i++)
                {
                    if(v.coins[i].Equals(vaultCoin))
                    {
                        return v.coins[i].value;
                    }
                }
            }
            return null;
        }


      
    }


}
