namespace Wetr.Simulator.Wpf.Interface
{
    /// <summary>
    /// Interface for Wetr.Simulator.Wpf.ViewModels
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