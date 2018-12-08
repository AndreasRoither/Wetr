using GalaSoft.MvvmLight;
using Wetr.Simulator.Wpf.Interface;

namespace Wetr.Simulator.Wpf.ViewModel
{
    /// <summary>
    /// This class contains properties that the SimulatorView can bind to
    /// </summary>
    /// <seealso cref="Wetr.Simulator.Wpf.Views.SimulationView"/>
    /// <seealso cref="Wetr.Simulator.Wpf.Interface.IWetrViewModelBase"/>
    public class SimulationViewModel : ViewModelBase, IWetrViewModelBase
    {
        public SimulationViewModel()
        {
        }

        public void CleanUp()
        {
            throw new System.NotImplementedException();
        }
    }
}