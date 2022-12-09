using System.Xml;
using System.Xml.Serialization;

namespace CurrencyGetterApp
{
    public class ExchangeOperations
    {
        public static readonly Dictionary<string, decimal> ExchangeRates = new();
        public static void LoadRates()
        {
            XmlDocument xmlDoc = new XmlDocument();
            //  xmlDoc.Load("http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml");
            xmlDoc.Load("https://tcmb.gov.tr/kurlar/today.xml");

            foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
            {              

                ExchangeRates.Add(node.ChildNodes[2].ChildNodes[0].Value, decimal.Parse(node.ChildNodes[3].ChildNodes[0].Value.Replace(".",",")));              
            }
        }
    }
}