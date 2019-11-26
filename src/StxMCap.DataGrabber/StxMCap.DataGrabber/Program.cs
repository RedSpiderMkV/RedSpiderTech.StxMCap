using System;
using System.Net;
using Newtonsoft.Json.Linq;
using StxMCap.DataGrabber.Factory;
using StxMCap.DataGrabber.Model;

namespace StxMCap.DataGrabber
{
    class Program
    {
        private static IMarketDataFactory _marketDataFactory;

        static void Main(string[] args)
        {
            _marketDataFactory = new MarketDataFactory();

            string[] symbols = { "MSFT", "BABA", "AMZN", "GOOG" };
            string header = string.Format("{0,-10} {1,-10} {2,-10}", "Symbol", "MarketCap", "CurrentPrice");
            Console.WriteLine(header);
            Console.WriteLine("".PadLeft(header.Length, '-'));

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
            IMarketData marketData = _marketDataFactory.GetMarketDataFromJson(jsonContent, symbol);
            Console.WriteLine(marketData);
        }
    }
}
