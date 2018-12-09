using System;

namespace Wetr.Simulator.Wpf.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}
