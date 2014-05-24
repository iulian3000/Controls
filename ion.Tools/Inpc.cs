namespace Ion.Tools
{
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [DebuggerStepThrough]

    /// <summary>
    /// Base class with INotifyPropertyChanged interface
    /// </summary>
    public class Inpc : INotifyPropertyChanged
    {
        /// <summary>
        /// PropertyChanged event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event
        /// </summary>
        /// <param name="propertyName"> The name of the property </param>
        public void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            var evt = this.PropertyChanged;
            if (evt != null)
            {
                evt(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}