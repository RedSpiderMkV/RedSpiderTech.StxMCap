using System;
using RedSpiderTech.YahooApi.DataGrabber.Model.Interface;

namespace StxMCap.DataGrabber.FileManagement
{
    public interface IOutputDataWriter : IDisposable
    {
        void AppendData(IMarketData marketData);
    }
}