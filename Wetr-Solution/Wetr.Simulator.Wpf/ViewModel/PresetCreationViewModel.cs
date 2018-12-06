using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using Wetr.Cockpit.Wpf.Model;
using Wetr.Simulator.Wpf.Interface;

namespace Wetr.Cockpit.Wpf.ViewModel
{
    /// <summary>
    /// This class contains properties that the PresetCreationView can bind to
    /// </summary>
    /// <seealso cref="Wetr.Simulator.Wpf.Views.PresetCreationView"/>
    /// <seealso cref="Wetr.Simulator.Wpf.Interface.IWetrViewModelBase"/>
    public class PresetCreationViewModel : ViewModelBase, IWetrViewModelBase
    {
        #region variables

        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                    Set(ref startDate, value);
            }
        }

        private DateTime endDate;

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                    Set(ref endDate, value);
            }
        }

        private string minVal;

        public string MinVal
        {
            get { return minVal; }
            set
            {
                if (minVal != value)
                    Set(ref minVal, value);
            }
        }

        private string maxVal;

        public string MaxVal
        {
            get { return maxVal; }
            set
            {
                if (maxVal != value)
                    Set(ref maxVal, value);
            }
        }

        private ObservableCollection<Preset> presetList;

        public ObservableCollection<Preset> PresetList
        {
            get { return presetList; }
            set
            {
                if (presetList != value)
                    Set(ref presetList, value);
            }
        }

        #endregion variables

        public PresetCreationViewModel()
        {
        }

        public void CleanUp()
        {
            throw new NotImplementedException();
        }
    }
}