using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace APMonogame
{
    //Deze klasse heeft als enig nut files uploaden via een externe file, attributes en de contents van deze attributes zijn 
    //te vinden in de .vke files (extension is gewoon mijn alias) daar laad ik content via de "Load=[]" keyword, bv.
    //ik wil mijn splashscreen images uploaden dus ik geef die eerst een attribute [image] dan in de contents zet ik
    //een reference naar mijn gamecontents die ik heb geupload bv. Load=[Image] \n [SplashScreen]
    public class FileManager
    {
        enum LoadType { Attributes, Contents };
        LoadType type;



        List<string> tempAttributes = new List<string>();
        List<string> tempContents = new List<string>();

        public void LoadContent(string filename, List<List<string>> attributes, List<List<string>> contents)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    //leest een lijn in mijn gereferenced file (.vke files)
                    string line = reader.ReadLine();
                    if (line.Contains("Load="))
                    {
                        tempAttributes = new List<string>();
                        line = line.Remove(0, line.IndexOf("=") + 1);
                        type = LoadType.Attributes;

                    }
                    else
                    {
                        type = LoadType.Contents;
                    }
                    tempContents = new List<string>();
                    //Hier filter ik de brackets die ik niet meer nodig heb om mijn contents te laden
                    string[] lineArray = line.Split(']');
                    foreach(string li in lineArray)
                    {
                        string newLine = li.Trim('[', ' ', ']');
                        if(newLine != String.Empty)
                        {
                            if (type == LoadType.Contents)                           
                                tempContents.Add(newLine);                            
                            else
                                tempAttributes.Add(newLine);
                        }
                    }
                    //check om te zien dat er geen lege spaties worden geupload
                    if (type == LoadType.Contents && tempContents.Count > 0)
                    {
                        contents.Add(tempContents);
                        attributes.Add(tempAttributes);

                    }
                }
            }

        }

    }
}
