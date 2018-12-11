using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using Wetr.Cockpit.Wpf.Interface;
using Wetr.Cockpit.Wpf.Views;

namespace Wetr.Cockpit.Wpf.ViewModel
{
    /// <summary>
    /// ViewModel for the LoginView
    /// </summary>
    /// <seealso cref="Wetr.Cockpit.Wpf.Views.LoginView"/>
    /// <seealso cref="Wetr.Cockpit.Wpf.Interface.IWetrViewModelBase"/>
    public class LoginViewModel : ViewModelBase, IWetrViewModelBase
    {
        public RelayCommand LoginCommand { get; private set; }

        public bool CanExecuteLoginCommand()
        {
            return true;
        }

        public void ExecuteLoginCommand()
        {
            MainWindow.SetContentControl(new MainContentView());
        }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(
                ExecuteLoginCommand,
                CanExecuteLoginCommand
            );
        }

        public void CleanUp()
        {
            throw new NotImplementedException();
        }
    }
}
