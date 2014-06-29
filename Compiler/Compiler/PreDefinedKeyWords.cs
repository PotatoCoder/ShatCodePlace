using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Compiler
{
    public class PreDefinedKeyWords
    {
        /*
         * This class creates the collections of pre-defined words and keywords for C# so we know what the user can not create or
         * use these words as variable names
         */
       public PreDefinedKeyWords()
        {
            List<string> words = new List<string>();
            for (int i = 0; i < File.ReadAllLines("PredefinedWords.txt").Length; i++)
            {
                words.Add(File.ReadAllLines("PredefinedWords.txt")[i]);
            }
        }
        
    }
}
