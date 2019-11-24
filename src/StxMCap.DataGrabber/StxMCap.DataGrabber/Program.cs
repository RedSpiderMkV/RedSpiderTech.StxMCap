using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace StxMCap.DataGrabber
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] symbols = { "MSFT", "BABA", "AMZN", "GOOG" };
            Console.WriteLine("Symbol\tMarketCap\tCurrentPrice");

            foreach (string symbol in symbols)
            {
                DisplayMarketData(symbol);
            }
        }

        private static void DisplayMarketData(string symbol)
        {
            string url = $"https://uk.finance.yahoo.com/quote/{symbol}?p={symbol}";

            var webClient = new WebClient();
            string pageContent = webClient.DownloadString(url);
            string[] pageContentLines = pageContent.Split('\n');

            string jsonContentHolder = null;
            foreach (string line in pageContentLines)
            {
                if (line.StartsWith("root.App.main"))
                {
                    jsonContentHolder = line.Split(new string[] { "root.App.main = " }, StringSplitOptions.None)[1];
                    break;
                }
            }

            string jsonContent = jsonContentHolder.Remove(jsonContentHolder.Length - 1);

            var jsonObject = JObject.Parse(jsonContent);
            var dataObject = jsonObject["context"]["dispatcher"]["stores"]["StreamDataStore"]["quoteData"][symbol];

            string symbol_value = dataObject["symbol"].Value<string>();
            string timestamp = dataObject["regularMarketTime"]["fmt"].Value<string>();
            string longName = dataObject.Value<string>("longName");
            string shortName = dataObject.Value<string>("shortName");
            string exchangeName = dataObject.Value<string>("fullExchangeName");
            string marketCapital = dataObject["marketCap"]["raw"].Value<string>();
            string current_price = dataObject["regularMarketPrice"]["raw"].Value<string>();
            double marketCapitalValue = double.Parse(marketCapital) / (1*Math.Pow(10,9));

            Console.WriteLine($"{symbol_value}\t{marketCapitalValue:F4}\t{current_price}");
        }
    }
}
