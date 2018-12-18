using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Cockpit.Wpf.Interface;
using Wetr.Cockpit.Wpf.Utility;

namespace Wetr.Cockpit.Wpf.ViewModel
{
    /// <summary>
    /// ViewModel for WeatherAnalysisView
    /// </summary>
    /// <seealso cref="Wetr.Cockpit.Wpf.Views.WeatherAnalysisView"/>
    /// <seealso cref="Wetr.Cockpit.Wpf.Interface.IWetrViewModelBase"/>
    public class WeatherAnalysisViewModel : ViewModelBase, IWetrViewModelBase
    {
        private NotifierManager notifierManager = new NotifierManager();

        public WeatherAnalysisViewModel() { }

        public void CleanUp()
        {
            base.Cleanup();
            notifierManager.Dispose();
        }
    }
}
