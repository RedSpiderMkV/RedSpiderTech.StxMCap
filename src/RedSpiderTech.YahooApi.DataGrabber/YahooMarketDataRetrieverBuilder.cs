using Autofac;
using RedSpiderTech.YahooApi.DataGrabber.Core.Implementation;
using RedSpiderTech.YahooApi.DataGrabber.Core.Interface;
using Serilog;

namespace RedSpiderTech.YahooApi.DataGrabber
{
    public class YahooMarketDataRetrieverBuilder : IYahooMarketDataRetrieverBuilder
    {
        #region Private Data

        private readonly IContainer _container;
        private readonly ILogger _logger;

        #endregion

        #region Public Methods

        public YahooMarketDataRetrieverBuilder(ILogger logger)
        {
            _logger = logger;

            _logger.Information("MarketDataRetrieverBuilder: Initialising internal container.");

            var builder = new ContainerBuilder();
            builder.Register(x => _logger).As<ILogger>();
            builder.RegisterType<ApiManager>().As<IApiManager>();
            builder.RegisterType<MarketDataFactory>().As<IMarketDataFactory>();
            builder.RegisterType<MarketDataRetriever>().As<IMarketDataRetriever>();
            builder.RegisterType<WebClientWrapperFactory>().As<IWebClientWrapperFactory>();
            _container = builder.Build();

            _logger.Information("MarketDataRetrieverBuilder: Internal container initialised.");
        }

        public IMarketDataRetriever GetMarketDataRetriever()
        {
            _logger.Information("MarketDataRetrieverBuilder: Retrieving MarketDataRetriever from container.");

            var marketDataRetriever = _container.Resolve<IMarketDataRetriever>();
            return marketDataRetriever;
        }

        #endregion
    }
}
