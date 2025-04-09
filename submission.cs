using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Linq;



/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Program
    {
        public static string xmlURL = "https://mmarschalik.github.io/cse445_a4/Hotels.xml";
        public static string xmlErrorURL = "https://mmarschalik.github.io/cse445_a4/HotelsErrors.xml";
        public static string xsdURL = "https://mmarschalik.github.io/cse445_a4/Hotels.xsdL";

        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);


            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);


            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            XmlSchemaSet schema = new XmlSchemaSet();
            schema.Add(null, xsdUrl);
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = schema;

            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

            //create Xml reader object
            XmlReader reader = XmlReader.Create(xmlUrl, settings);
            while (reader.Read()) ; 
            //Console.WriteLine("The XML file is valid for the given xsd file");
            Console.WriteLine("No Error");
            //return "No Error" if XML is valid. Otherwise, return the desired exception message.
            return "No Error";
        }
        private static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            Console.WriteLine("Validation Error: {0}", e.Message);
        }


        public static string Xml2Json(string xmlUrl) //parser
        {
            //XDocument xmlDoc = new XDocument();
            XDocument xmlDoc = XDocument.Parse(xmlUrl);
            
            //xmlDoc = XmlDocument.Load(xmlURL);
            //xmlDoc.Load(xmlUrl);
            
            string jsonText = JsonConvert.SerializeXNode(xmlDoc);
           // XmlDocument doc = JsonConvert.DeserializeXmlNode(jsonText);

            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
            return jsonText;

        }
    }

}
