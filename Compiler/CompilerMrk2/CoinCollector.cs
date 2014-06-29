using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerMrk2
{
    public class CoinCollector
    {
        /*
         * This class finds variable declerations and creates coins for them
         * 6-23-2014 As of creatign this we might need to reowkr this when doing comparsions with = sign
         */
        //Creates local variables from BuildingBlocks

        public void Collect(CoreBlock core)
        {
            foreach (ClassBlock c in core.classBlocks)
            {
                foreach (BuildingBlock b in c.buildingBlocks)
                {
                    CreateBBCoins(b);
                    CreateCLCoins(b, core);
                    CreateUserCCoins(b, core);
                    CreateInstancedCoins(b, core);
                }
            }
        }
        //Creates Variables
        public void CreateBBCoins(BuildingBlock b)
        {
            //Create Variables from declerations within method
            for (int i = 0; i < b.linesOfCode.Count(); i++)
            {
                if (b.linesOfCode[i].Contains('='))
                {
                    //Split up data type and name
                    string[] v1 = b.linesOfCode[i].Split('=');
                    //Split up name and data type
                    string[] v2 = v1[0].Split(' ');
                    b.vb.Deposit("variables", new Coin(v2[1], v1[1], v2[0]));
                }
            }
            //Create Variables from parameters
            for (int i = 0; i < b.parameters.Count(); i++)
            {
                b.vb.Deposit("variables", new Coin(b.parameters[i].Item2, b.parameters[i].Item1, b.parameters[i].Item3));
            }

            //Need to Create a Variable Vault even if non exist
            if(b.vb.Repository.Keys.Contains("variables"))
            {

            }
            else
            {
                b.vb.Repository.Add("variables", new Vault());
            }



        }
        // Code Library coins
        public void CreateCLCoins(BuildingBlock b, CoreBlock c)
        {
            for (int i = 0; i < b.linesOfCode.Count(); i++)
            {
                if (b.linesOfCode[i].Contains("new"))
                {
                    CreateClassCoins(b, c, b.linesOfCode[i]);
                    continue;
                }
                //So if we found a method/function call
                if (b.linesOfCode[i].Contains("."))
                {
                    if (b.linesOfCode[i].Contains("Console"))
                    {
                        //Split up name and parameters
                        string[] m1 = b.linesOfCode[i].Split('(', ')');
                        string s = m1[0];
                        if (s.Contains("Console.Read"))
                        {
                            CreateIOCoins(b, m1);
                        }
                        else
                        {
                            m1[0] = s.Replace('.', '_').ToLower();
                            //Split up parameters
                            string[] m2 = m1[1].Split(',');
                            List<object> coinParameters = new List<object>();
                            for (int j = 0; j < m2.Count(); j++)
                            {
                                if (!m2[j].Equals(""))
                                {
                                    coinParameters.Add(m2[j]);
                                }
                            }
                            b.vb.Deposit("Calls", new Coin(m1[0], coinParameters));
                        }
                    }
                }
            }
        }

        //Custom User Method Coins
        public void CreateUserCCoins(BuildingBlock b, CoreBlock c)
        {
            //Check/Find Method Building block
            for (int i = 0; i < c.classBlocks.Count(); i++)
            {
                for (int j = 0; j < c.classBlocks[i].buildingBlocks.Count(); j++)
                {
                    for (int ii = 0; ii < b.linesOfCode.Count(); ii++)
                    {
                        if (b.linesOfCode[ii].Contains('(') || b.linesOfCode[ii].Contains(')'))
                        {
                            string[] u1 = b.linesOfCode[ii].Split(')', '(');
                            string u = u1[0];
                            if (u.Equals(c.classBlocks[i].buildingBlocks[j].Name))
                            {
                                //Grab parameters
                                string[] u2 = u1[1].Split(',');
                                List<object> coinParameters = new List<object>();
                                for (int k = 0; k < u2.Count(); k++)
                                {
                                    if (!u2[k].Equals(""))
                                    {
                                        coinParameters.Add(u2[k]);
                                    }
                                }
                                b.vb.Deposit("Calls", new Coin(u1[0], coinParameters, c.classBlocks[i].buildingBlocks[j]));
                            }
                        }
                    }

                }
            }
        }


        //Collect User Input

        public void CreateIOCoins(BuildingBlock b, string[] w1)
        {
            string s = w1[0];
            w1[0] = s.Replace('.', '_').ToLower();
            //Split up parameters
            if (w1[0].Contains('='))
            {
                //Split up data type and name
                string[] v1 = w1[0].Split('=');
                //Split up name and data type
                string[] v2 = v1[0].Split(' ');
                b.vb.Deposit("Calls", new Coin(v1[1], v2[1], v2[0], b));
            }


        }
        //When user creates a new instance of a class
        // This also needs to handle new Lists,Array etc
        public void CreateClassCoins(BuildingBlock b, CoreBlock c, string s)
        {
            for (int i = 0; i < b.linesOfCode.Count(); i++)
            {
                if (b.linesOfCode[i].Contains("new"))
                {
                    //Ex: PrintClass print = new PrintClass();
                    string[] a1 = b.linesOfCode[i].Split('=');
                    //Grab Class and name that you are assinging it
                    string[] a2 = a1[0].Split(' ');
                    string[] u1 = a1[1].Split(')', '(');
                    //Grab parameters
                    string[] u2 = u1[1].Split(',');
                    List<object> coinParameters = new List<object>();
                    for (int k = 0; k < u2.Count(); k++)
                    {
                        if (!u2[k].Equals(""))
                        {
                            coinParameters.Add(u2[k]);
                        }
                    }

                    /*
                     * a2[0] = Class Name, a2[1] = Given Name, and we have parameters if any
                     * Need to Assign coin class block associted with instance of class
                     */
                    ClassBlock blockToAssign = new ClassBlock();
                    for (int j = 0; j < c.classBlocks.Count(); j++)
                    {
                        if (a2[0].Equals(c.classBlocks[j].Name))
                        {
                            blockToAssign = c.classBlocks[j];
                            break;
                        }
                    }

                    b.vb.Deposit("Calls", new Coin(a2[0], a2[1], blockToAssign, coinParameters));
                }
            }
        }

        // We need to create coins for methods called using instanced classes in the current running block
        public void CreateInstancedCoins(BuildingBlock b, CoreBlock c)
        {
            for (int i = 0; i < c.classBlocks.Count(); i++)
            {
                for (int j = 0; j < b.vb.Repository["Calls"].coins.Count(); j++)
                {
                    if (b.vb.Repository["Calls"].coins[j].class_block != null)
                    {
                        //Find the Coin that was created for the new instance
                        if (b.vb.Repository["Calls"].coins[j].class_block.Equals(c.classBlocks[i]))
                        {
                            // Grab its given name
                            string name = b.vb.Repository["Calls"].coins[j].givenName;
                            for (int k = 0; k < b.linesOfCode.Count(); k++)
                            {
                                //Find the Line that can be used for calling methods
                                if (b.linesOfCode[k].Contains('.'))
                                {
                                    // Make sure line has Name of called class
                                    if (b.linesOfCode[k].Contains(name))
                                    {
                                        //Time to Create Method Coin for currently running block XD hahaha
                                        string[] s1 = b.linesOfCode[k].Split('.');

                                        string[] u1 = s1[1].Split(')', '(');
                                        //Grab parameters
                                        string[] u2 = u1[1].Split(',');
                                        List<object> coinParameters = new List<object>();
                                        for (int z = 0; z < u2.Count(); z++)
                                        {
                                            if (!u2[z].Equals(""))
                                            {
                                                coinParameters.Add(u2[z]);
                                            }
                                        }
                                        //Need to Grab Building block from ClassBlock and Assign into to new coin
                                        BuildingBlock blockToAssign = new BuildingBlock();
                                        blockToAssign = c.classBlocks[i].buildingBlocks.Find(n => n.Name == u1[0].Trim());
                                        b.vb.Deposit("Calls", new Coin(u1[0], coinParameters, blockToAssign));
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }

        //Creates Global variables
        public void CreateCCoins(ClassBlock cBlock)
        {

        }
    }
}
