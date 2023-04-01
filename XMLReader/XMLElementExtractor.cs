
namespace XMLReader;

public static class XMLElementExtractor
{

    private static void WriteFile(List<string> lines, string path, string number, string year, string documentType, string currentSection)
    {
        string pathAndName = path + $"{documentType}{number}-{year}-{currentSection}.txt";
        using (StreamWriter writetext = new StreamWriter(pathAndName))
        {
            // converts list of strings to string
            string str = string.Join(" ", lines);
            writetext.WriteLine(str);
        }
    }

    /// <summary>
    /// This method is used to get the "Paragraf" element and subelements 
    /// for normal documents like https://www.retsinformation.dk/eli/lta/2023/14/xml and https://www.retsinformation.dk/eli/lta/2020/195/xml
    /// </summary>
    /// <param name="paragraf"></param>
    /// <param name="path"></param>
    public static void ExtractXMLElementParagraf(XmlNodeList paragraf, string path, string number, string year, string documentType)
    {
        if (paragraf.Count != 0)
        {
            for (int i = 0; i < paragraf.Count; i++)
            {
                // gets the current § without whitespace and the § symbol
                string currentSection = CleanText.Clean(paragraf[i].FirstChild.InnerText.Replace(" ", ""));

                List<string> lines = new List<string>();
                // paragraf child nodes
                XmlNodeList paragrafChildNodes = paragraf[i].ChildNodes;
                for (int j = 0; j < paragrafChildNodes.Count; j++)
                {
                    XmlNodeList child = paragrafChildNodes[j].ChildNodes;
                    for (int k = 0; k < child.Count; k++)
                    {
                        lines.Add(child[k].InnerText);
                    }
                }
                WriteFile(lines, path, number, year, documentType, currentSection);
            }
        }
               
    }



    public static void ExtractXMLElementAendringCentreretParagraf(XmlNodeList aendringCentreretParagraf, string path, string number, string year, string documentType)
    {
        if (aendringCentreretParagraf.Count != 0)
        {
            XmlNodeList aendringCentreretParagrafChildren = aendringCentreretParagraf[0].ChildNodes;
            // gets the current § without whitespace and the § symbol
            string currentSection = CleanText.Clean(aendringCentreretParagrafChildren[0].FirstChild.InnerText.Replace(" ", ""));
            List<string> lines = new List<string>();
            for (int i = 0; i < aendringCentreretParagrafChildren.Count; i++)
            {
                XmlNode node = aendringCentreretParagrafChildren[i];

                if (node.Name == "AendringsNummer")
                {
                    XmlNodeList childNodes = node.ChildNodes;
                    for (int j = 0; j < childNodes.Count; j++)
                    {
                        XmlNode child = childNodes[j];
                        lines.Add(child.InnerText);
                    }
                }
                else
                {
                    lines.Add(node.InnerText);
                }

            }
            WriteFile(lines, path, number, year, documentType, currentSection);
        }      
    }




}
