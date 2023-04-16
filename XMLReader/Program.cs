


int docYear = 2008;

string dataDirectory = $@"C:\Users\Cleve\Desktop\cSharp-xml\XMLReader\data";
if (Directory.Exists(dataDirectory) == false)
{
    Directory.CreateDirectory(dataDirectory);
}


for (int i = 172; i < 200; i++)
{


    string URL = $"https://www.retsinformation.dk/eli/lta/{docYear}/{i}/xml";
    XmlTextReader reader = new XmlTextReader(URL);
    XmlDocument doc = new XmlDocument();

    doc.Load(reader);

    string status = doc.GetElementsByTagName("Status").Item(0).InnerText;
    string number = doc.GetElementsByTagName("Number").Item(0).InnerText;
    string year = doc.GetElementsByTagName("Year").Item(0).InnerText;
    string documentType = doc.GetElementsByTagName("DocumentType").Item(0).InnerText.Substring(0, 3);


    XmlNodeList aendringCentreretParagraf = doc.GetElementsByTagName("AendringCentreretParagraf");
    XmlNodeList paragraf = doc.GetElementsByTagName("Paragraf");

    string docDirectory = @$"C:\Users\Cleve\Desktop\cSharp-xml\XMLReader\data\{documentType}{number}-{year}";

    if (Directory.Exists(docDirectory) == false)
    {
        Directory.CreateDirectory(docDirectory);
    }


    string path = @$"C:\Users\Cleve\Desktop\cSharp-xml\XMLReader\data\{documentType}{number}-{year}\";

    XMLElementExtractor.ExtractXMLElementParagrafV2(paragraf, path, number, year, documentType);
    XMLElementExtractor.ExtractXMLElementAendringCentreretParagraf(aendringCentreretParagraf, path, number, year, documentType);

    Thread.Sleep(2000);



}



