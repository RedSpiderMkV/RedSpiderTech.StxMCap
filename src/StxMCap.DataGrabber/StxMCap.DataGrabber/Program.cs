using System;
using StxMCap.DataGrabber.ApiManagement;
using StxMCap.DataGrabber.Factory;
using StxMCap.DataGrabber.Model;

namespace StxMCap.DataGrabber
{
    class Program
    {
        private static IMarketDataFactory _marketDataFactory;
        private static IApiManager _apiManager;
        private static IWebClientWrapperFactory _webClientWrapperFactory;

        static void Main(string[] args)
        {
            _marketDataFactory = new MarketDataFactory();
            _webClientWrapperFactory = new WebClientWrapperFactory();
            _apiManager = new ApiManager(_webClientWrapperFactory);

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
            string jsonContent = _apiManager.GetJsonData(symbol);
            IMarketData marketData = _marketDataFactory.GetMarketDataFromJson(jsonContent, symbol);

            Console.WriteLine(marketData);
        }
    }
}
