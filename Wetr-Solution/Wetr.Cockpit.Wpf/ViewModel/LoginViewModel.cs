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
    /// ViewModel for the LoginView
    /// </summary>
    /// <seealso cref="Wetr.Cockpit.Wpf.Views.LoginView"/>
    /// <seealso cref="Wetr.Cockpit.Wpf.Interface.IWetrViewModelBase"/>
    public class LoginViewModel : ViewModelBase, IWetrViewModelBase
    {
        public LoginViewModel()
        {

        }

        public void CleanUp()
        {
            throw new NotImplementedException();
        }
    }
}
