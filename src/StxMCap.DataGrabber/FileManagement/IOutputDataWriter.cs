using System;
using StxMCap.DataGrabber.Model;

namespace StxMCap.DataGrabber.FileManagement
{
    public interface IOutputDataWriter : IDisposable
    {
        void AppendData(IMarketData marketData);
    }
}