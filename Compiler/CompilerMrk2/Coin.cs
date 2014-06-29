using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerMrk2
{
    /*
     * Variables,Method calls,Loops are all going to be stored in some for of Vault each with thier own type of coin
     * Each class/method will have a bank which has all of the vaults
     */
    public class Coin
    {
        //Coin Properties
        public List<object> parameters = new List<object>();
        public BuildingBlock building_block;
        public string name;
        public object value;
        public string typeOfCoin;
        public string varType;
        public string var_name;
        //Variable Coins
        public Coin()
        {

        }

        public Coin(string name,object value,string varType)
        {
            this.varType = varType;
            this.typeOfCoin = "variable";
            this.name = name;
            this.value = value;
        }
        //Method Call Coins
        public Coin(string name, List<object> p)
        {
            this.typeOfCoin = "coreMethod";
            this.name = name;
            this.parameters = p;
        }
        //Custom Method Calls
        public Coin(string name,List<object> p,BuildingBlock b)
        {
            this.typeOfCoin = "userMethod";
            this.building_block = b;
            this.name = name;
            this.parameters = p;
        }
        //User Input
        public Coin(string varname, string name, string varType,BuildingBlock b)
        {
            this.building_block = b;
            this.var_name = varname;
            this.typeOfCoin = "userInput";
            this.name = name;
            this.varType = varType;
        }
        //Creating a New instance of a class or object
        public string className;
        public string givenName;
        public ClassBlock class_block;
        public Coin(string className,string givenName,ClassBlock class_block,List<object> parameters)
        {
            this.typeOfCoin = "newInstance";
            this.class_block = class_block;
            this.className = className;
            this.givenName = givenName;
            this.parameters = parameters;
        }
    }
}
