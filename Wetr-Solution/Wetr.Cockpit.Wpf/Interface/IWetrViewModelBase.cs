using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wetr.Cockpit.Wpf.Interface
{
    /// <summary>
    /// Interface for Wetr.Cockpit.Wpf.ViewModels
    /// <para>Specifies what the ViewModels have to implement</para>
    /// </summary>
    public interface IWetrViewModelBase
    {
        /// <summary>
        /// Should be called when the class is not needed anymore
        /// </summary>
        void CleanUp();
    }
}
