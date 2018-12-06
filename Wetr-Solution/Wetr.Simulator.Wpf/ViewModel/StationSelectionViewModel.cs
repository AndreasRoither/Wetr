using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using Wetr.Domain;
using Wetr.Simulator.Wpf.Interface;

namespace Wetr.Simulator.Wpf.ViewModel
{
    /// <summary>
    /// This class contains Properties that the StationSelectionView can bind to
    /// </summary>
    /// <seealso cref="Wetr.Simulator.Wpf.Views.StationSelectionView"/>
    /// <seealso cref="Wetr.Simulator.Wpf.Interface.IWetrViewModelBase"/>
    public class StationSelectionViewModel : ViewModelBase, IWetrViewModelBase
    {
        #region variables

        private ObservableCollection<Station> availableStations;

        public ObservableCollection<Station> AvailableStations
        {
            get { return availableStations; }
            set
            {
                if (availableStations != value)
                {
                    Set(ref availableStations, value);
                }
            }
        }

        private ObservableCollection<Station> selectedStations;

        public ObservableCollection<Station> SelectedStations
        {
            get { return selectedStations; }
            set
            {
                if (selectedStations != value)
                {
                    Set(ref selectedStations, value);
                }
            }
        }

        #endregion variables

        public StationSelectionViewModel()
        {
        }

        public void CleanUp()
        {
            throw new System.NotImplementedException();
        }
    }
}