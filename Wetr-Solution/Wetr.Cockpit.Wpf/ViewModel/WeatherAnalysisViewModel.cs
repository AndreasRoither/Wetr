using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Cockpit.Wpf.Utility;

namespace Wetr.Cockpit.Wpf.ViewModel
{
    /// <summary>
    /// ViewModel for WeatherAnalysisView
    /// </summary>
    /// <seealso cref="Wetr.Cockpit.Wpf.Views.WeatherAnalysisView"/>
    public class WeatherAnalysisViewModel : ViewModelBase
    {
        private NotifierManager notifierManager = new NotifierManager();

        public WeatherAnalysisViewModel() { }

        public override void Cleanup()
        {
            base.Cleanup();
            notifierManager.Dispose();
        }
    }
}
