using System;
using System.ComponentModel;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Lifetime.Clear;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace Wetr.Simulator.Wpf.Utility
{
    /// <summary>
    /// Manages Notifications
    /// </summary>s
    public class NotifierManager : INotifyPropertyChanged, IDisposable
    {
        private readonly Notifier _notifier;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NotifierManager()
        {
            _notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.BottomRight,
                    offsetX: 25,
                    offsetY: 25);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(5),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(6));

                cfg.Dispatcher = Application.Current.Dispatcher;

                cfg.DisplayOptions.TopMost = false;
                cfg.DisplayOptions.Width = 250;
            });

            _notifier.ClearMessages(new ClearAll());
        }

        internal void ClearMessages(string msg)
        {
            _notifier.ClearMessages(new ClearByMessage(msg));
        }

        /// <summary>
        /// Show Information notification
        /// </summary>
        /// <param name="message">Toast Message</param>
        public void ShowInformation(string message)
        {
            _notifier.ShowInformation(message);
        }

        /// <summary>
        /// Show Information notification with options
        /// </summary>
        /// <param name="message">Toast Message</param>
        /// <param name="opts">Notification options</param>
        public void ShowInformation(string message, MessageOptions opts)
        {
            _notifier.ShowInformation(message, opts);
        }

        /// <summary>
        /// Show success notification
        /// </summary>
        /// <param name="message">Toast Message</param>
        public void ShowSuccess(string message)
        {
            _notifier.ShowSuccess(message);
        }

        /// <summary>
        /// Show success notification with options
        /// </summary>
        /// <param name="message">Toast Message</param>
        /// <param name="opts">Notification options</param>
        public void ShowSuccess(string message, MessageOptions opts)
        {
            _notifier.ShowSuccess(message, opts);
        }

        /// <summary>
        /// Show warning notification
        /// </summary>
        /// <param name="message">Toast Message</param>
        public void ShowWarning(string message)
        {
            _notifier.ShowWarning(message);
        }

        /// <summary>
        /// Show warning notification with options
        /// </summary>
        /// <param name="message">Toast Message</param>
        /// <param name="opts">Notification options</param>
        public void ShowWarning(string message, MessageOptions opts)
        {
            _notifier.ShowWarning(message, opts);
        }

        /// <summary>
        /// Show error notification
        /// </summary>
        /// <param name="message">Toast Message</param>
        public void ShowError(string message)
        {
            _notifier.ShowError(message);
        }

        /// <summary>
        /// Show error notification with options
        /// </summary>
        /// <param name="message">Toast Message</param>
        /// <param name="opts">Notification options</param>
        public void ShowError(string message, MessageOptions opts)
        {
            _notifier.ShowError(message, opts);
        }

        /// <summary>
        /// Clear all notifications
        /// </summary>
        public void ClearAll()
        {
            _notifier.ClearMessages(new ClearAll());
        }

        /// <summary>
        /// Should be called when NotifierManager is not needed anymore
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                _notifier.Dispose();
            }
            // free native resources if there are any.
        }
    }
}
