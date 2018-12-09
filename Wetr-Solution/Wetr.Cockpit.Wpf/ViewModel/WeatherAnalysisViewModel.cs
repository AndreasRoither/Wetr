using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Cockpit.Wpf.Interface;

namespace Wetr.Cockpit.Wpf.ViewModel
{
    /// <summary>
    /// ViewModel for WeatherAnalysisView
    /// </summary>
    /// <seealso cref="Wetr.Cockpit.Wpf.Views.WeatherAnalysisView"/>
    /// <seealso cref="Wetr.Cockpit.Wpf.Interface.IWetrViewModelBase"/>
    public class WeatherAnalysisViewModel : ViewModelBase, IWetrViewModelBase
    {
        public WeatherAnalysisViewModel() { }

        public void CleanUp()
        {
            throw new NotImplementedException();
        }
    }
}
