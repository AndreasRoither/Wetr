using GalaSoft.MvvmLight;

namespace Wetr.Cockpit.ViewModels
{
    public class StartPageViewModel : ViewModelBase
    {
        private string _helloWorld;

        public string HelloWorld
        {
            get
            {
                return _helloWorld;
            }
            set
            {
                Set(() => HelloWorld, ref _helloWorld, value);
            }
        }

        public StartPageViewModel()
        {
            HelloWorld = IsInDesignMode
                ? "Runs in design mode"
                : "Runs in runtime mode";
        }
    }
}