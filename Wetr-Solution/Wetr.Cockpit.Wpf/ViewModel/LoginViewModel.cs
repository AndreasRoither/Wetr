using Common.Dal.Ado;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Windows.Controls;
using Wetr.BusinessLogic;
using Wetr.Cockpit.Wpf.Interface;
using Wetr.Cockpit.Wpf.Views;
using Wetr.Dal.Ado;
using Wetr.Domain;

namespace Wetr.Cockpit.Wpf.ViewModel
{
    /// <summary>
    /// ViewModel for the LoginView
    /// </summary>
    /// <seealso cref="Wetr.Cockpit.Wpf.Views.LoginView"/>
    /// <seealso cref="Wetr.Cockpit.Wpf.Interface.IWetrViewModelBase"/>
    public class LoginViewModel : ViewModelBase, IWetrViewModelBase
    {
        #region variables
        public User loggedInUser { get; set; }

        private UserManager userManager;

        public RelayCommand<object> LoginCommand { get; private set; }

        private string loginMessage;

        public string LoginMessage
        {
            get { return loginMessage; }
            set {
                if (loginMessage != value)
                    Set(ref loginMessage, value);
            }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set {
                if (email != value)
                    Set(ref email, value);
            }
        }

        #endregion variables

        public LoginViewModel()
        {
            userManager = ManagerLocator.GetUserManagerInstance;

            LoginCommand = new RelayCommand<object>(
                ExecuteLoginCommand,
                CanExecuteLoginCommand
            );
        }

        #region functions

        public bool CanExecuteLoginCommand(object obj)
        {
            return true;
        }

        // we should not use binding to the password directly
        // http://gigi.nullneuron.net/gigilabs/security-risk-in-binding-wpf-passwordbox-password/
        // instead:
        // https://stackoverflow.com/questions/15390727/passwordbox-and-mvvm/15391318#15391318
        public async void ExecuteLoginCommand(object obj)
        {
            User user = new User();
            user.UserId = 1;
            loggedInUser = user;

            MainWindow.SetContentControl(new MainContentView());

            /*
            PasswordBox pwBox = obj as PasswordBox;
            loggedInUser = await userManager.UserCredentialValidation(email, pwBox.Password);

            if (loggedInUser == null)
            {
                LoginMessage = "Login failed!";
            }
            else
            {
                LoginMessage = string.Empty;
                MainWindow.SetContentControl(new MainContentView());
            }*/
        }        

        public void CleanUp()
        {
            base.Cleanup();
        }

        #endregion functions
    }
}
